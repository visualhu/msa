using NSP.Common;
using NSP.Common.Events;
using NSP.Domain.Events.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Domain
{
    public class CustomerProfile : AggregateRoot<Guid>
    {
        private readonly IList<Guid> myFriends = new List<Guid>();

        public CustomerProfile()
        {
            ApplyEvent(new CustomerCreatedEvent(Guid.Empty));
        }

        public CustomerProfile(Guid id)
        {
            ApplyEvent(new CustomerCreatedEvent(id));
        }

        public CustomerProfile(Guid id, string name, string passwd, string email, string referralId)
        {
            ApplyEvent(new CustomerCreatedEvent(id, name, passwd, email, referralId));
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string ReferralId { get; private set; }

        public IEnumerable<Guid> MyFriends => myFriends;

        public void ChangeEmail(string email)
        {
            ApplyEvent(new CustomerEmailChangedEvent(this.Id, email));
        }

        public void SendInvitation(CustomerProfile toCustomer, string invitationLetter)
        {
            ApplyEvent(new InvitationSentEvent(this.Id, this.Id, toCustomer.Id, this.Name, toCustomer.Name, invitationLetter));
        }

        public void AddFriends(Guid friendId)
        {
            ApplyEvent(new FriendAddedEvent(this.Id, friendId));
        }

        [InlineEventHandler]
        private void HandleCustomerCreatedEvent(CustomerCreatedEvent evt)
        {
            this.Id = (Guid)evt.AggregateRootKey;
            this.Password = evt.Password;
            this.Name = evt.Name;
            this.Email = evt.Email;
            this.ReferralId = evt.ReferralId;
        }

        [InlineEventHandler]
        private void HandleChangeEmailEvent(CustomerEmailChangedEvent evt)
        {
            this.Email = evt.Email;
        }

        [InlineEventHandler]
        private void HandleAddFriendEvent(FriendAddedEvent evnt)
        {
            this.myFriends.Add(evnt.FriendId);
        }

    }
}
