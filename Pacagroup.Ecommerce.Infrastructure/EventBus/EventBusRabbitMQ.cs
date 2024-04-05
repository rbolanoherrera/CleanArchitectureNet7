using MassTransit;
using Pacagroup.Ecommerce.Application.Interface.Infrastructure;

namespace Pacagroup.Ecommerce.Infrastructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Para enviar un mensaje o evento a un Broker de mensajeria, esn este caso a RabbitMQ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        public async void Publish<T>(T @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}