using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public class OrSpecification<T> : CompositerSpecification<T>
    {
        public OrSpecification(Specification<T> left, Specification<T> right) : base(left, right)
        {

        }
        public override Expression<Func<T, bool>> Expression
        {
            get
            {
                return Left.Expression.Or(Right.Expression);
            }
        }
    }

}

