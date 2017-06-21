using NSP.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Domain.Events.Customer
{
    public class CustomerEmailChangedEvent : DomainEvent
    {
        public string Email { get; set; }

        public CustomerEmailChangedEvent(object aggregateRootKey, string email) : base(aggregateRootKey)
        {
            this.Email = email;
        }
    }
}
