﻿using Async.Api.Data;
using Async.Application.Common.EventBus.Broker;
using Async.Application.Common.Identity.Notififcation.Broker;
using Async.Application.Common.Identity.Notififcation.Service;
using Async.Application.Common.Identity.Serialize;
using Async.Application.Common.Identity.Service;
using Async.Application.Common.Identity.Verification.Service;
using Async.Infrostructure.Common.Cashing.Broker;
using Async.Infrostructure.Common.EventBus.Broker;
using Async.Infrostructure.Common.EventBus.Service;
using Async.Infrostructure.Common.Identity.Service;
using Async.Infrostructure.Common.Notification.Broker;
using Async.Infrostructure.Common.Notification.EventSubcriber;
using Async.Infrostructure.Common.Notification.Service;
using Async.Infrostructure.Common.Serializres;
using Async.Infrostructure.Common.Setting;
using Async.Infrostructure.Common.Verification.Service;
using Async.Persistance.Cashing.Broker;
using Async.Persistance.DataContext;
using Async.Persistance.Repository;
using Async.Persistance.Repository.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Async.Api.Configuration;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddSerializers(this WebApplicationBuilder builder)
    {
        // register json serialization settings
        builder.Services.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>();

        return builder;
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        // register fluent validation
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        // register automapper
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        // register cache settings
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        // register distributed cache
        builder.Services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
                options.InstanceName = "Caching.SimpleInfra";
            }
        );
        builder.Services.AddSingleton<ICacheBroker, RedisDistributedCacheBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        // register cache settings
        builder.Services.Configure<RabbitMqConnectionSettings>(builder.Configuration.GetSection(nameof(RabbitMqConnectionSettings)))
            .Configure<NotificationSubscriberSettings>(builder.Configuration.GetSection(nameof(NotificationSubscriberSettings)));

        // register brokers
        builder.Services.AddSingleton<IRabbitMqConnectionProvider, RabbitMqConnectionProvider>()
            .AddSingleton<IEventBusBroker, RabbitMqEventBusBroker>();

        // register general background service
        builder.Services.AddHostedService<EventBusBackgroundService>();

        // register event subscribers
        builder.Services.AddSingleton<IEventSubscriber, NotificationSubscriber>();

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<TemplateRenderingSettings>(builder.Configuration.GetSection(nameof(TemplateRenderingSettings)))
            .Configure<SmtpEmailSenderSettings>(builder.Configuration.GetSection(nameof(SmtpEmailSenderSettings)))
            .Configure<TwilioSmsSenderSettings>(builder.Configuration.GetSection(nameof(TwilioSmsSenderSettings)))
            .Configure<NotificationSettings>(builder.Configuration.GetSection(nameof(NotificationSettings)));

        // register persistence
        builder.Services.AddDbContext<NotificationDbContext>(
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("NotificationsDatabaseConnection"))
        );

        builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>().AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();

        // register brokers
        builder.Services.AddScoped<IEmailSenderBroker, SmtpEmailSenderBroker>().AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();

        // register data access foundation services
        builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>().AddScoped<IEmailHistoryService, EmailHistoryService>();

        // register helper foundation services
        builder.Services.AddScoped<IEmailSenderService, EmailSenderService>().AddScoped<IEmailRenderingService, EmailRenderingService>();

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations
        builder.Services.Configure<PasswordValidationSettings>(builder.Configuration.GetSection(nameof(PasswordValidationSettings)));
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        // register db contexts
        //builder.Services.AddDbContext<IdentityDbContext>(
        //    options => options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDatabaseConnection"))
        //);

        // register repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserSettingsRepository, UserSettingsRepository>()
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>();

        // register helper foundation services
        builder.Services.AddTransient<IPasswordHasherService, PasswordHasherService>()
            .AddTransient<IPasswordGeneratorService, PasswordGeneratorService>()
            .AddTransient<IAccessTokenGeneratorService, AccessTokenGeneratorService>();

        // register foundation data access services
        builder.Services.AddScoped<IUserService, UserService>()
            .AddScoped<IUserSettingsService, UserSettingsService>()
            .AddScoped<IAccessTokenService, AccessTokenService>();

        // register other higher services
        builder.Services.AddScoped<IAccountAggregatorService, AccountAggregatorService>()
            .AddScoped<IAuthAggregationService, AuthAggregationService>();

        // register authentication handlers
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ??
                          throw new InvalidOperationException("JwtSettings is not configured.");

        // add authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidateLifetime = jwtSettings.ValidateLifetime,
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                }
            );

        return builder;
    }

    private static WebApplicationBuilder AddVerificationInfrastructure(this WebApplicationBuilder builder)
    {
        // register configurations
        builder.Services.Configure<VerificationSettings>(builder.Configuration.GetSection(nameof(VerificationSettings)));

        // register repositories
        builder.Services.AddScoped<IUserInfoVerificationCodeRepository, UserInfoVerificationCodeRepository>();

        // register foundation data access services
        builder.Services.AddScoped<IUserInfoVerificationCodeService, UserInfoVerificationCodeService>();

        // register other higher services
        builder.Services.AddScoped<IVerificationProcessingService, VerificationProcessingService>();

        return builder;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();

        return builder;
    }

    private static async Task<WebApplication> MigrateDatabaseAsync(this WebApplication app)
    {
        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        //await scopeFactory.MigrateAsync<IdentityDbContext>();
        await scopeFactory.MigrateAsync<NotificationDbContext>();

        return app;
    }

    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }

    private static WebApplication UseIdentityInfrastructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}
