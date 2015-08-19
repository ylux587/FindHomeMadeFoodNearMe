namespace FineHomeMadeFoodNearMe.Services.DataAccess
{
    using Microsoft.SqlServer.Server;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface ITableBinder
    {
        SqlParameter BindTable(string parameterName, string typeName, IEnumerable<SqlDataRecord> rows);
    }
}