﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Specifications
{
    internal sealed class ExpressionSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _expression;

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this._expression = expression;
        }

        public override Expression<Func<T, bool>> Expression
        {
            get
            {
                return this._expression;
            }
        }
    }
}
