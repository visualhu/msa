using NSP.Common.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Querying.MySqlClient
{
    internal sealed class MySqlWhereClauseBuilder<TTableObject> : WhereClauseBuilder<TTableObject>
        where TTableObject : class, new()
    {
        protected override char ParameterChar => '?';
    }
}
