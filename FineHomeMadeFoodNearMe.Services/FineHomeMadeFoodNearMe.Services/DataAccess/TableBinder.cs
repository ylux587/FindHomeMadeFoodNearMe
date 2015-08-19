using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

namespace FineHomeMadeFoodNearMe.Services.DataAccess
{
    public class TableBinder
    {
        public SqlParameter BindTable(
            String parameterName,
            String typeName,
            IEnumerable<SqlDataRecord> rows,
            bool convertEmptyToNull = true)
        {
            if (convertEmptyToNull && !rows.Any())
            {
                rows = null;
            }

            SqlParameter param = m_sqlCommand.Parameters.AddWithValue(parameterName, rows);

            param.TypeName = typeName;
            param.SqlDbType = SqlDbType.Structured;

            return param;
        }
    }
}