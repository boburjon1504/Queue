using Async.Application.Common.EventBus.Broker;
using Async.Application.Common.Identity.Serialize;
using Async.Domain.Common.Events;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Async.Infrostructure.Common.EventBus.Broker;
public class RabbitMqEventBusBroker(
    IRabbitMqConnectionProvider rabbitMqConnectionProvider,
    IJsonSerializationSettingsProvider jsonSerializationSettingsProvider
) : IEventBusBroker
{
    public async ValueTask PublishAsync<TEvent>(TEvent @event, string exchange, string routingKey, CancellationToken cancellationToken) where TEvent : Event
    {
        var channel = await rabbitMqConnectionProvider.CreateChannelAsync();

        var properties = new BasicProperties
        {
            Persistent = true
        };

        var serializerSettings = jsonSerializationSettingsProvider.Get(true);
        serializerSettings.ContractResolver = new DefaultContractResolver();
        serializerSettings.TypeNameHandling = TypeNameHandling.All;

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, serializerSettings));
        await channel.BasicPublishAsync(exchange, routingKey, properties, body);
    }
}
