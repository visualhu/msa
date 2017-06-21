﻿using NSP.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Messaging
{
    public interface IEventConsumer : IMessageConsumer
    {
        IEnumerable<IDomainEventHandler> EventHandlers { get; }
    }
}
