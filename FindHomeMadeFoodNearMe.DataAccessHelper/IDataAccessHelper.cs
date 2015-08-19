namespace FindHomeMadeFoodNearMe.DataAccessHelper
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public interface IDataAccessHelper
    {
        void ExecuteNonQuery(string storedProcedureName, int timeout, params SqlParameter[] commandParameters);

        object ExecuteScalar(string storedProcedureName, int timeout, params SqlParameter[] commandParameters);

        IList<T> ExecuteReader<T>(
            string storedProcedureName,
            Func<SqlDataReader, T> processMethod,
            SqlTransaction transaction,
            int timeout,
            params SqlParameter[] commandParameters);
    }
}
