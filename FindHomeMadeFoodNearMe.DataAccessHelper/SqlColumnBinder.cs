namespace FindHomeMadeFoodNearMe.DataAccessHelper
{
    using System;
    using System.Data.Common;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;

    public sealed class SqlColumnBinder
    {
        private const int UnknowColumnOrdinal = -2;
        private const int ColumnNotFound = -1;

        public SqlColumnBinder(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                throw new ArgumentNullException("columnName");
            }
            this.ColumnName = columnName;
        }

        public string ColumnName { get; private set;}

        public int Ordinal { get; private set; }

        public string GetString(IDataReader reader, bool allowNulls)
        {
            InitColumn(reader);
            if (allowNulls && reader.IsDBNull(Ordinal))
            {
                return null;
            }
            else
            {
                return reader.GetString(Ordinal);
            }
        }

        public string GetString(IDataReader reader, string missingColumnValue)
        {
            InitColumn(reader, useFindOrdinal: true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return null;
            }
            else
            {
                return reader.GetString(Ordinal);
            }
        }

        public short GetInt16(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetInt16(Ordinal);
        }

        public short GetInt16(IDataReader reader, Int16 nullValue)
        {
            InitColumn(reader);

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetInt16(Ordinal);
            }
        }

        public short GetInt16(IDataReader reader, Int16 nullValue, Int16 missingColumnValue)
        {
            InitColumn(reader, useFindOrdinal: true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetInt16(Ordinal);
            }
        }

        public int GetInt32(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetInt32(Ordinal);
        }

        public int GetInt32(IDataReader reader, int nullValue)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetInt32(Ordinal);
            }
        }

        public int GetInt32(IDataReader reader, int nullValue, int missingColumnValue)
        {
            InitColumn(reader, true);
            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetInt32(Ordinal);
            }
        }

        public long GetInt64(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetInt64(Ordinal);
        }

        public long GetInt64(IDataReader reader, long nullValue)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetInt64(Ordinal);
            }
        }

        public long GetInt64(IDataReader reader, long nullValue, long missingColumnValue)
        {
            InitColumn(reader, true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetInt64(Ordinal);
            }
        }

        public byte GetByte(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetByte(Ordinal);
        }

        public byte GetByte(IDataReader reader, byte nullValue)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetByte(Ordinal);
            }
        }

        public byte GetByte(IDataReader reader, byte nullValue, byte missingColumnValue)
        {
            InitColumn(reader, true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetByte(Ordinal);
            }
        }

        public double GetDouble(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetDouble(Ordinal);
        }

        public double GetDouble(IDataReader reader, double nullValue)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetDouble(Ordinal);
            }
        }

        public float GetFloat(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetFloat(Ordinal);
        }

        public float GetFloat(IDataReader reader, float nullValue)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetFloat(Ordinal);
            }
        }

        public float GetFloat(IDataReader reader, float nullValue, float missingColumnValue)
        {
            InitColumn(reader, true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }
            else
            {
                return reader.GetFloat(Ordinal);
            }
        }

        public bool GetBoolean(IDataReader reader)
        {
            InitColumn(reader);
            return reader.GetBoolean(Ordinal);
        }

        public bool GetBoolean(IDataReader reader, bool nullValue)
        {
            InitColumn(reader);
            return reader.IsDBNull(Ordinal) ? nullValue : reader.GetBoolean(Ordinal);
        }

        public bool GetBoolean(IDataReader reader, bool nullValue, out bool isNull)
        {
            InitColumn(reader);
            isNull = reader.IsDBNull(Ordinal);
            return isNull ? nullValue : reader.GetBoolean(Ordinal);
        }

        public byte[] GetBytes(IDataReader reader, bool allowNulls)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return new byte[0];
            }
            else
            {
                if (reader is SqlDataReader)
                {
                    return ((SqlDataReader)reader).GetSqlBinary(Ordinal).Value;
                }
                else
                {
                    int maxBufferSize = 1024 * 1024;
                    byte[] buffer = new byte[maxBufferSize];
                    int bytesRead = (int)reader.GetBytes(Ordinal, 0, buffer, 0, maxBufferSize);
                    Array.Resize(ref buffer, bytesRead);
                    return buffer;
                }
            }
        }

        public int GetBytes(IDataReader reader, long dataOffset, byte[] buffer, int bufferIndex, int length)
        {
            InitColumn(reader);
            return (int)reader.GetBytes(Ordinal, dataOffset, buffer, bufferIndex, length);
        }

        public Guid GetGuid(IDataReader reader)
        {
            return GetGuid(reader, false);
        }

        public Guid GetGuid(IDataReader reader, bool allowNulls)
        {
            InitColumn(reader);
            if (allowNulls && reader.IsDBNull(Ordinal))
            {
                return Guid.Empty;
            }
            else
            {
                return reader.GetGuid(Ordinal);
            }
        }

        public Guid GetGuid(IDataReader reader, bool allowNulls, Guid missingColumnValue)
        {
            InitColumn(reader, true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (allowNulls && reader.IsDBNull(Ordinal))
            {
                return Guid.Empty;
            }
            else
            {
                return reader.GetGuid(Ordinal);
            }
        }

        public DateTime GetDateTime(IDataReader reader)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return DateTime.MinValue;
            }
            else
            {
                var date = reader.GetDateTime(Ordinal);
                date = new DateTime(date.Ticks, DateTimeKind.Utc);

                return date;
            }
        }

        public DateTimeOffset GetDateTimeOffset(SqlDataReader reader)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return DateTimeOffset.MinValue;
            }
            else
            {
                var date = reader.GetDateTimeOffset(Ordinal);

                return date;
            }
        }

        public DateTime GetDateTime(IDataReader reader, DateTime missingColumnValue)
        {
            InitColumn(reader, true);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return DateTime.MinValue;
            }
            else
            {
                var date = reader.GetDateTime(Ordinal);
                date = new DateTime(date.Ticks, DateTimeKind.Utc);

                return date;
            }
        }
        
        public TimeSpan GetTimeSpan(SqlDataReader reader)
        {
            InitColumn(reader);
            TimeSpan value = reader.GetTimeSpan(Ordinal);
            return value;
        }

        public TimeSpan GetTimeSpan(SqlDataReader reader, TimeSpan nullValue)
        {
            InitColumn(reader);

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }

            var value = reader.GetTimeSpan(Ordinal);
            return value;
        }

        public TimeSpan GetTimeSpan(SqlDataReader reader, TimeSpan nullValue, TimeSpan missingColumnValue)
        {
            InitColumn(reader);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }

            var value = reader.GetTimeSpan(Ordinal);
            return value;
        }

        public decimal GetDecimal(SqlDataReader reader)
        {
            InitColumn(reader);
            var value = reader.GetDecimal(Ordinal);
            return value;
        }

        public decimal GetDecimal(SqlDataReader reader, decimal nullValue)
        {
            InitColumn(reader);

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }

            var value = reader.GetDecimal(Ordinal);
            return value;
        }

        public decimal GetDecimal(SqlDataReader reader, decimal nullValue, decimal missingColumnValue)
        {
            InitColumn(reader);

            if (Ordinal == ColumnNotFound)
            {
                return missingColumnValue;
            }

            if (reader.IsDBNull(Ordinal))
            {
                return nullValue;
            }

            var value = reader.GetDecimal(Ordinal);
            return value;
        }

        public object GetObject(IDataReader reader)
        {
            InitColumn(reader);
            if (reader.IsDBNull(Ordinal))
            {
                return null;
            }
            else
            {
                return reader.GetValue(Ordinal);
            }
        }

        public bool IsInitialized()
        {
            return (Ordinal != UnknowColumnOrdinal);
        }

        public bool IsNull(IDataReader reader)
        {
            InitColumn(reader);
            return reader.IsDBNull(Ordinal);
        }

        public int GetOrdinal(IDataReader reader)
        {
            if (Ordinal == UnknowColumnOrdinal)
            {
                InitColumn(reader);
            }
            return Ordinal;
        }

        public bool ColumnExists(IDataReader reader)
        {
            if (Ordinal == UnknowColumnOrdinal)
            {
                InitColumn(reader, true);
            }

            return Ordinal >= 0;
        }

        private void InitColumn(IDataReader reader, bool useFindOrdinal = false)
        {
            if (!IsInitialized())
            {
                if (useFindOrdinal)
                {
                    Ordinal = FindOrdinal(reader, ColumnName);
                }
                else
                {
                    Ordinal = reader.GetOrdinal(ColumnName);
                }
            }
        }

        /// <summary>
        /// Gets the column ordinal given the name of the column.
        /// This method is less efficient than DbDataReader.GetOrdinal(), but does not 
        /// throw IndexOutOfRangeException if column does not exist.
        /// </summary>
        /// <param name="reader">Reader.</param>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>Column ordinal if column was found; otherwise -1.</returns>
        private static int FindOrdinal(IDataReader reader, string columnName)
        {
            int ordinal;

            for (ordinal = reader.FieldCount - 1; ordinal >= 0; --ordinal)
            {
                string name = reader.GetName(ordinal);

                if (columnName.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }
            }

            return ordinal;
        }
    }
}
