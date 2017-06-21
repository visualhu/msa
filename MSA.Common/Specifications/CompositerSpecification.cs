using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public abstract class CompositerSpecification<T> : Specification<T>, ICompositerSpecification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public CompositerSpecification(Specification<T> left, Specification<T> right)
        {
            this._left = left;
            this._right = right;
        }

        public Specification<T> Left
        {
            get { return _left; }
        }

        public Specification<T> Right
        {
            get { return _right; }
        }
    }
}
