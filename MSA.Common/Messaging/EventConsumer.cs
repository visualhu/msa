using NSP.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Messaging
{
    public class EventConsumer : DisposableObject, IEventConsumer
    {
        private readonly IEnumerable<IDomainEventHandler> _eventHandlers;
        private readonly IMessageSubscriber _subscriber;
        private bool disposed;

        public EventConsumer(IMessageSubscriber subscriber, IEnumerable<IDomainEventHandler> eventHandlers)
        {
            this._subscriber = subscriber;
            this._eventHandlers = eventHandlers;

            _subscriber.MessageReceived += async (sender, e) =>
            {
                if (this._eventHandlers != null)
                {
                    foreach (var handler in this._eventHandlers)
                    {
                        var handlerType = handler.GetType();
                        var messageType = e.Message.GetType();
                        var methodInfoQuery = from method in handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                              let parameters = method.GetParameters()
                                              where method.Name == "HandleAsync" &&
                                              method.ReturnType == typeof(Task) &&
                                              parameters.Length == 1 &&
                                              parameters[0].ParameterType == messageType
                                              select method;
                        var methodInfo = methodInfoQuery.FirstOrDefault();
                        if (methodInfo != null)
                        {
                            await (Task)methodInfo.Invoke(handler, new[] { e.Message });
                        }
                    }
                }
            };
        }

        public IEnumerable<IDomainEventHandler> EventHandlers => _eventHandlers;
        public IMessageSubscriber Subscriber => _subscriber;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    this._subscriber.Dispose();
                    disposed = true;
                }
            }
        }
    }
}
