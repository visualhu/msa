using NSP.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Repositories
{
    public abstract class DomainRepository : IDomainRepository
    {
        private readonly IMessagePublisher _messagePublisher;
        public DomainRepository(IMessagePublisher messagePublisher)
        {
            this._messagePublisher = messagePublisher;
        }

        public async Task SaveAsync<TKey, TAggregateRoot>(TAggregateRoot aggregateRoot, bool purge = true)
            where TKey : IEquatable<TKey>
            where TAggregateRoot : class, IAggregateRoot<TKey>, new()
        {
            await this.SaveAggregateAsync<TKey, TAggregateRoot>(aggregateRoot);
            foreach (var evt in aggregateRoot.UncommittedEvents)
            {
                _messagePublisher.Publish(evt);
            }

            if (purge)
            {
                ((IPurgeable)aggregateRoot).Purge();
            }
        }


        public async Task<TAggregateRoot> GetByKeyAsync<TKey, TAggregateRoot>(TKey key)
            where TKey : IEquatable<TKey>
            where TAggregateRoot : class, IAggregateRoot<TKey>, new()
        {
            var result = await this.GetAggregateAsync<TKey, TAggregateRoot>(key);
            ((IPurgeable)result).Purge();
            return result;
        }


        protected abstract Task SaveAggregateAsync<TKey, TAggregateRoot>(TAggregateRoot aggregateRoot) where TKey : IEquatable<TKey> where TAggregateRoot : class, IAggregateRoot<TKey>, new();

        protected abstract Task<TAggregateRoot> GetAggregateAsync<TKey, TAggregateRoot>(TKey aggregateRootKey) where TKey : IEquatable<TKey> where TAggregateRoot : class, IAggregateRoot<TKey>, new();

    }
}
