using MySql.Data.MySqlClient;
using NSP.Common.Querying;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSP.Querying.MySqlClient
{
    public sealed class MySqlTableDataGateway : TableDataGateway
    {
        private readonly string _connectionString;
        public MySqlTableDataGateway(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override DbCommand CreateCommand(string sql, DbConnection connection)
        {
            return new MySqlCommand(sql, (MySqlConnection)connection);
        }

        protected override DbConnection CreateDatabaseConnection()
        {
            return new MySqlConnection(this._connectionString);
        }

        protected override DbParameter CreateParameter()
        {
            return new MySqlParameter();
        }

        protected override WhereClauseBuilder<TTableObject> CreateWhereClauseBuilder<TTableObject>()
        {
            return new MySqlWhereClauseBuilder<TTableObject>();
        }
    }
}
