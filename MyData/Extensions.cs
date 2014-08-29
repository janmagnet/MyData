using System.Data;

namespace MyData
{
    public static class Extensions
    {
        /// <summary>
        /// Return the value of the specified field or null if value is eqivalent to DBNull type.
        /// </summary>
        /// <param name="record">The data record to operate on.</param>
        /// <param name="index">The index of the field to find.</param>
        /// <returns>The System.Object which will contain the field value upon return.</returns>
        public static object GetNullableValue(this System.Data.IDataRecord record, int index)
        {
            if (record.IsDBNull(index)) return null;
            return record.GetValue(index);
        }

        public static string GetString(this System.Data.IDataRecord record, int index, string defaultValue)
        {
            object value = record.GetNullableValue(index);
            if (value == null) return defaultValue;
            return value.ToString();
        }

        public static int GetParsedInt32(this System.Data.IDataRecord record, int index, int defaultValue)
        {
            object value = record.GetNullableValue(index);
            if (value == null) return defaultValue;
            int result;
            return (int.TryParse(value.ToString(), out result) ? result : defaultValue);
        }

        public static int GetParsedInt32(this System.Data.IDataRecord record, int index)
        {
            object value = record.GetNullableValue(index);
            return int.Parse(value.ToString());
        }

        public static string GetNullableString(this System.Data.IDataRecord record, int index)
        {
            object value = record.GetNullableValue(index);
            return (value == null ? null : value.ToString());
        }

        public static IDbDataParameter AddParameter(this IDbCommand command, object value, DbType paramType)
        {
            IDbDataParameter parameter = command.CreateParameter();
            command.Parameters.Add(parameter);
            parameter.DbType = paramType;
            parameter.Value = value;

            return parameter;
        }

        public static IDbDataParameter AddParameter(this IDbCommand command, string name, object value, DbType paramType)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = paramType;
            parameter.Value = value;
            command.Parameters.Add(parameter);

            return parameter;
        }
    }
}
