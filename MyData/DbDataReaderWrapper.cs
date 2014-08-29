using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace MyData
{
    public class DbDataReaderWrapper : DataReader
    {
        private readonly DbDataReader reader;
        private readonly IDictionary<string, int> headerMap;

        private readonly int fieldCount;

        public DbDataReaderWrapper(DbDataReader reader)
        {
            this.reader = reader;
            this.headerMap = new Dictionary<string, int>();

            fieldCount = reader.FieldCount; // TODO Is .VisibleFieldCount more applicable?
            for (int i = 0; i < fieldCount; i++)
            {
                string headerName = reader.GetName(i);
                this.headerMap.Add(headerName, i);
            }
        }

        public override bool Next()
        {
            return reader.Read();
        }

        public override string this[int fieldIndex]
        {
            get { return reader[fieldIndex].ToString(); }
        }

        public override string this[string fieldName]
        {
            get { return reader[fieldName].ToString(); }
        }

        protected override IDataRecord GetDataEntry()
        {
            object[] values = new object[fieldCount];
            int valuesCount = reader.GetValues(values);

            string[] recordData = values.Select(x => x.ToString()).ToArray();

            return new ArrayBasedDataRecord(recordData, headerMap);
        }
    }
}
