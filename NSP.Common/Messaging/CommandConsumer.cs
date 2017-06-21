using NSP.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Messaging
{
    public class CommandConsumer : DisposableObject, ICommandConsumer
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IMessageSubscriber _subscriber;
        private bool disposed;

        public CommandConsumer(IMessageSubscriber subscriber, IEnumerable<ICommandHandler> commandHandlers)
        {
            this._subscriber = subscriber;
            this._commandHandlers = commandHandlers;
            subscriber.MessageReceived += async (sender, e) =>
            {
                if (this._commandHandlers != null)
                {
                    foreach (var handler in this._commandHandlers)
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

        public IEnumerable<ICommandHandler> CommandHandlers => _commandHandlers;

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
