namespace FindHomeMadeFoodNearMe.DataAccessHelper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    ///     Data access helper class
    /// </summary>
    public sealed class DataAccessHelper : IDataAccessHelper
    {
        private readonly string _connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DataAccessHelper" /> class.
        ///     The data access helper.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public DataAccessHelper(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString", "connectionString cannot be null or empty");
            }

            this._connectionString = connectionString;
        }

        /// <summary>
        ///     Execute non query
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure name</param>
        /// <param name="timeout">The timeout value</param>
        /// <param name="commandParameters">The parameters for stored procedure</param>
        public void ExecuteNonQuery(string storedProcedureName, int timeout, params SqlParameter[] commandParameters)
        {
            this.ExecuteCommand(storedProcedureName, timeout, false, commandParameters);
        }

        /// <summary>
        ///     Execute scalar
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure name</param>
        /// <param name="timeout">The timeout value</param>
        /// <param name="commandParameters">The parameters for stored procedure</param>
        /// <returns>
        ///     Expected scalar value returned from data source
        /// </returns>
        public object ExecuteScalar(string storedProcedureName, int timeout, params SqlParameter[] commandParameters)
        {
            return this.ExecuteCommand(storedProcedureName, timeout, true, commandParameters);
        }

        /// <summary>
        ///     Execute reader
        /// </summary>
        /// <typeparam name="T">The type of returned instance</typeparam>
        /// <param name="storedProcedureName">The stored procedure name</param>
        /// <param name="processMethod">The process method</param>
        /// <param name="transaction">The sql db transction object</param>
        /// <param name="timeout">The timeout value</param>
        /// <param name="commandParameters">The parameters for stored procedure</param>
        /// <returns>The<see><cref>IList</cref> of <typeparamref name="T" /> entities</see></returns>
        public IList<T> ExecuteReader<T>(
            string storedProcedureName,
            Func<SqlDataReader, T> processMethod,
            SqlTransaction transaction,
            int timeout,
            params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName cannot be null or empty");
            }

            using (var command = new SqlCommand(storedProcedureName) { CommandType = CommandType.StoredProcedure })
            {
                if (timeout > 0)
                {
                    command.CommandTimeout = timeout;
                }

                if (commandParameters != null && commandParameters.Length > 0)
                {
                    command.Parameters.AddRange(commandParameters);
                }

                using (var connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    command.Connection = connection;
                    var collection = new List<T>();
                    if (transaction != null)
                    {
                        command.Transaction = transaction;
                    }
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (processMethod != null)
                            {
                                collection.Add(processMethod(reader));
                            }
                        }
                    }
                    if (transaction != null)
                    {
                        transaction.Commit();
                    }
                    return collection;
                }
            }
        }

        /// <summary>
        ///     Execute command
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure name</param>
        /// <param name="timeout">The timeout value</param>
        /// <param name="isExecuteScalarCall">A value to determine if scalar value is expected to return</param>
        /// <param name="commandParameters">The parameters of stored procedure</param>
        /// <returns>
        ///     The expected returned value if <paramref name="isExecuteScalarCall" /> is true. Otherwise, return null
        /// </returns>
        private object ExecuteCommand(
            string storedProcedureName,
            int timeout,
            bool isExecuteScalarCall,
            params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentNullException("storedProcedureName", "storedProcedureName cannot be null or empty");
            }

            using (var command = new SqlCommand(storedProcedureName) { CommandType = CommandType.StoredProcedure })
            {
                if (timeout > 0)
                {
                    command.CommandTimeout = timeout;
                }

                if (commandParameters != null && commandParameters.Length > 0)
                {
                    command.Parameters.AddRange(commandParameters);
                }

                using (var connection = new SqlConnection(this._connectionString))
                {
                    connection.Open();

                    command.Connection = connection;
                    using (var transaction = connection.BeginTransaction())
                    {
                        command.Transaction = transaction;
                        var retValue = isExecuteScalarCall ? command.ExecuteScalar() : command.ExecuteNonQuery();

                        transaction.Commit();
                        return retValue;
                    }
                }
            }
        }
    }
}
