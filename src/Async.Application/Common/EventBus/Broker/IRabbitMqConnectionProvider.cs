using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Async.Application.Common.EventBus.Broker;
public interface IRabbitMqConnectionProvider
{
    ValueTask<IChannel> CreateChannelAsync();
}
