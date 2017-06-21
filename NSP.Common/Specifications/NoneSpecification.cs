using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public sealed class NoneSpecification<T> : Specification<T>
    {
        public override Expression<Func<T, bool>> Expression => _ => false;

    }
}
