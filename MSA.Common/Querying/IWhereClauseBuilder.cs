using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Querying
{
    public interface IWhereClauseBuilder<TTableObject> where TTableObject : class, new()
    {
        WhereClauseBuildResult BuildWhereClause(Expression<Func<TTableObject, bool>> expression);
    }
}
