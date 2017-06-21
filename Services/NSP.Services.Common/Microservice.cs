using NSP.Common.Messaging;
using NSP.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Services.Common
{
    public abstract class Microservice : Service
    {
        private readonly ICommandConsumer _commandConsumer;
        private readonly IEventConsumer _eventConsumer;
        private bool disposed;

        public Microservice(ICommandConsumer commandConsumer, IEventConsumer eventConsumer)
        {
            this._commandConsumer = commandConsumer;
            this._eventConsumer = eventConsumer;
        }

        public ICommandConsumer CommandConsumer => _commandConsumer;


        public IEventConsumer EventConsumer => _eventConsumer;


        public override void Start(object[] args)
        {
            this._commandConsumer.Subscriber.Subscribe();
            this._eventConsumer.Subscriber.Subscribe();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    this._commandConsumer.Dispose();
                    this._eventConsumer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}