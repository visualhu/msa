using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSP.Common.Events;

namespace NSP.Common
{
    public interface IAggregateRoot<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        IEnumerable<IDomainEvent> UncommittedEvents { get; }

        void Replay(IEnumerable<IDomainEvent> events);
    }
}
