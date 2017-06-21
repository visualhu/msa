using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Common.Querying
{
    public sealed class WhereClauseBuildResult
    {

        public string WhereClause { get; set; }
        public Dictionary<string, object> ParameterValues { get; set; }

        public WhereClauseBuildResult() { }

        public WhereClauseBuildResult(string whereClause, Dictionary<string, object> parameterValues)
        {
            this.WhereClause = whereClause;
            this.ParameterValues = parameterValues;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(WhereClause);
            sb.Append(Environment.NewLine);
            ParameterValues.ToList().ForEach(kvp =>
            {
                sb.Append(string.Format("{0}=[{1}] {Type: [2]}", kvp.Key, kvp.Value.ToString(), kvp.Value.GetType().FullName));
                sb.Append(Environment.NewLine);
            });
            return sb.ToString();
        }
    }
}
