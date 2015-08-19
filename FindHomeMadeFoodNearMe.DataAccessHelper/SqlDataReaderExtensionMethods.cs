namespace FindHomeMadeFoodNearMe.DataAccessHelper
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    ///     Defined extension methods
    /// </summary>
    public static class SqlDataReaderExtensionMethods
    {
        /// <summary>
        ///     Read integer value from data reader
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A integer value of specific column name
        /// </returns>
        public static int ReadInt32Value(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            return reader.GetInt32(reader.GetOrdinal(columnName));
        }

        /// <summary>
        ///     Read long value from data reader
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A long value of specific column name
        /// </returns>
        public static long ReadInt64Value(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            return reader.GetInt64(reader.GetOrdinal(columnName));
        }

        /// <summary>
        ///     Read DateTime value from data reader and specify it as UTC time
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A datetime value of specific column name, specifying the datetime kind as UTC
        /// </returns>
        public static DateTime ReadUtcDateTimeValue(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            return DateTime.SpecifyKind(reader.GetDateTime(reader.GetOrdinal(columnName)), DateTimeKind.Utc);
        }

        /// <summary>
        ///     Read DateTimeOffset value from data reader
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A datetime value of specific column name
        /// </returns>
        public static DateTimeOffset ReadDateTimeOffsetValue(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            return reader.GetDateTimeOffset(reader.GetOrdinal(columnName));
        }

        /// <summary>
        ///     Read string value from data reader
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A string value of specific column name
        /// </returns>
        public static string ReadStringValue(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            return reader.GetString(reader.GetOrdinal(columnName));
        }

        /// <summary>
        ///     Read Guid value from data reader
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A string value of specific column name
        /// </returns>
        public static Guid ReadGuidValue(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            return reader.GetGuid(reader.GetOrdinal(columnName));
        }

        /// <summary>
        ///     Read bytes value from reader
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <param name="columnName">The column name</param>
        /// <returns>
        ///     A byte array value of specific column name
        /// </returns>
        public static byte[] ReadBytesValue(this SqlDataReader reader, string columnName)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            if (!reader.HasColumn(columnName))
            {
                throw new ArgumentException("Cannot find column in data reader. ColumnName: " + columnName, "columnName");
            }
            var sqlBinary = reader.GetSqlBinary(reader.GetOrdinal(columnName));
            return sqlBinary.Value;
        }

        private static bool HasColumn(this IDataRecord reader, string columnName)
        {
            for (var index = 0; index < reader.FieldCount; index++)
            {
                if (reader.GetName(index).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
