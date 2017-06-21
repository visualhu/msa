using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec;
        public NotSpecification(Specification<T> spec)
        {
            this._spec = spec;
        }
        public override Expression<Func<T, bool>> Expression
        {
            get
            {
                return this._spec.Expression.Not();
            }
        }
    }

}

