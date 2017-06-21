using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public abstract class Specification<T>
    {
        public virtual bool IsSatisfiedBy(T obj)
        {
            return this.Expression.Compile()(obj);
        }

        public Specification<T> And(Specification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public Specification<T> Or(Specification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public Specification<T> AndNot(Specification<T> other)
        {
            return new AndNotSpecification<T>(this, other);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public abstract Expression<Func<T, bool>> Expression { get; }

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification) => specification.Expression;

        public static implicit operator Specification<T>(Expression<Func<T, bool>> expression) => new ExpressionSpecification<T>(expression);

    }
}
