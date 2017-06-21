using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NSP.Common.Events;

namespace NSP.Common
{
    public abstract class AggregateRoot<TKey> : IAggregateRoot<TKey>, IPurgeable where TKey : IEquatable<TKey>
    {
        private readonly Queue<IDomainEvent> _uncommittedEvents = new Queue<IDomainEvent>();

        public TKey Id { get; set; }

        public IEnumerable<IDomainEvent> UncommittedEvents => _uncommittedEvents;

        public void Replay(IEnumerable<IDomainEvent> events)
        {
            ((IPurgeable)this).Purge();
            foreach (var evt in events)
            {
                this.ApplyEvent(evt);
            }
        }

        protected void ApplyEvent<TEvent>(TEvent evt) where TEvent : IDomainEvent
        {
            var eventHandlerMethods = from m in this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                      let parameters = m.GetParameters()
                                      where m.IsDefined(typeof(InlineEventHandlerAttribute))
                                      && m.ReturnType == typeof(void)
                                      && parameters.Length == 1
                                      && parameters[0].ParameterType == evt.GetType()
                                      select m;
            evt.AggregateRootType = this.GetType().FullName;
            foreach (var eventHandlerMethod in eventHandlerMethods)
            {
                eventHandlerMethod.Invoke(this, new object[] { evt });
            }
            this._uncommittedEvents.Enqueue(evt);
        }

        void IPurgeable.Purge()
        {
            if (this._uncommittedEvents.Count > 0)
            {
                this._uncommittedEvents.Clear();
            }
        }

    }
}
