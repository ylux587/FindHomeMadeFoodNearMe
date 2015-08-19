namespace FineHomeMadeFoodNearMe.Services.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.SqlClient;
    using Microsoft.SqlServer.Server;

    public static class TableBinder
    {
        public static SqlParameter BindTable(string parameterName, string typeName, IEnumerable<SqlDataRecord> rows, bool convertEmptyToNull = true)
        {
            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }
            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentNullException("typeName");
            }

            var param = new SqlParameter(parameterName, convertEmptyToNull && !rows.Any() ? null : rows);

            param.TypeName = typeName;
            param.SqlDbType = SqlDbType.Structured;

            return param;
        }
    }
}