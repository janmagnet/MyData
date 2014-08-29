using System;
using System.Collections.Generic;

namespace MyData
{
    public class ArrayBasedDataRecord : IDataRecord
    {
        private string[] lineData;
        private IDictionary<string, int> headerMap;

        public ArrayBasedDataRecord(string[] lineData, IDictionary<string, int> headerMap)
        {
            this.lineData = lineData;
            this.headerMap = headerMap;
        }

        public string this[int fieldIndex]
        {
            get
            {
                return this.lineData[fieldIndex];
            }
        }

        public string this[string fieldName]
        {
            get
            {
                if (this.headerMap == null)
                {
                    throw new InvalidOperationException("No field headers found.");
                }

                int headerIndex;
                if (!this.headerMap.TryGetValue(fieldName, out headerIndex))
                {
                    throw new ArgumentException("Specified field name '" + fieldName + "' could not be found.");
                }

                return this[headerIndex];
            }

        }

        public bool HasField(string fieldName)
        {
            if (this.headerMap == null)
            {
                throw new InvalidOperationException("No field headers found.");
            }

            return this.headerMap.ContainsKey(fieldName);
        }
    }
}
