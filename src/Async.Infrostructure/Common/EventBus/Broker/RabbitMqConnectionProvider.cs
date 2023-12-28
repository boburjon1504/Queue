using Async.Application.Common.EventBus.Broker;
using Async.Infrostructure.Common.Setting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.EventBus.Broker;
public class RabbitMqConnectionProvider(IOptions<RabbitMqConnectionSettings> rabbitMqConnectionSettings) : IRabbitMqConnectionProvider
{
    private readonly ConnectionFactory _connectionFactory = new()
    {
        HostName = rabbitMqConnectionSettings.Value.HostName,
        Port = rabbitMqConnectionSettings.Value.Port
    };

    private IConnection? _connection;
    public async ValueTask<IChannel> CreateChannelAsync()
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync();

        return await _connection.CreateChannelAsync();
    }
}
