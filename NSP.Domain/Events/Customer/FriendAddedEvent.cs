using NSP.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Domain.Events.Customer
{
    public class FriendAddedEvent : DomainEvent
    {
        public Guid FriendId { get; set; }

        protected FriendAddedEvent() : base() { }

        public FriendAddedEvent(object aggregateRootKey, Guid friendId)
            : base(aggregateRootKey)
        {
            this.FriendId = friendId;
        }
    }
}
