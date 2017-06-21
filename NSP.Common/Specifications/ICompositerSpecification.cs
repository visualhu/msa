using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public interface ICompositerSpecification<T>
    {
        Specification<T> Left { get; }

        Specification<T> Right { get; }
    }
}
