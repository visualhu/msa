﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    public class AndNotSpecification<T> : CompositerSpecification<T>
    {
        public AndNotSpecification(Specification<T> left, Specification<T> right) : base(left, right)
        {

        }
        public override Expression<Func<T, bool>> Expression
        {
            get
            {
                return Left.Expression.And(Right.Expression.Not());
            }
        }
    }

}
