using NSP.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Domain.Events.Customer
{
    public class CustomerCreatedEvent : DomainEvent
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ReferralId { get; set; }

        protected CustomerCreatedEvent() : base() { }

        public CustomerCreatedEvent(object aggregateRootKey) : base(aggregateRootKey) { }

        public CustomerCreatedEvent(object aggregateRootKey, string name, string password, string email, string referralId) : base(aggregateRootKey)
        {
            this.Name = name;
            this.Password = password;
            this.ReferralId = referralId;
            this.Email = email;
        }

    }
}
