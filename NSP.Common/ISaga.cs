using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSP.Common.Events;

namespace NSP.Common
{
    public interface ISaga<TKey, TMessage> : IAggregateRoot<TKey> where TKey : IEquatable<TKey> where TMessage : IDomainEvent
    {
        void Transit(TMessage message);
    }
}
