using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyData
{
    public class CsvReader : DataReader, IDisposable
    {
        private readonly StreamReader reader;
        private readonly string delimiter;
        private readonly bool streamOwner;
        private string[] lineData;
        private IDictionary<string, int> headerMap;
        private IDataRecord currentEntry;

        public CsvReader(StreamReader reader, string delimiter, bool hasHeaders)
        {
            this.reader = reader;
            this.streamOwner = false;

            this.delimiter = delimiter;
            if (hasHeaders)
            {
                this.ReadHeaders();
            }
        }

        public CsvReader(string fileName, string delimiter, bool hasHeaders)
            : this(fileName, delimiter, hasHeaders, Encoding.UTF8)
        {
        }

        public CsvReader(string fileName, string delimiter, bool hasHeaders, Encoding encoding)
        {
            this.reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), encoding);
            this.streamOwner = true;

            this.delimiter = delimiter;
            if (hasHeaders)
            {
                this.ReadHeaders();
            }
        }

        private void ReadHeaders()
        {
            this.headerMap = new Dictionary<string, int>();		
		
            string headerLine = this.reader.ReadLine();
            string[] headers = headerLine.Split(new[] { this.delimiter }, StringSplitOptions.None);
            for (int i = 0; i < headers.Length; i++) {
                this.headerMap.Add(headers[i].Trim(), i);
            }
        }

        public override string this[string fieldName]
        {
            get {
                if (this.lineData == null) throw new InvalidOperationException("Reading from the first entry has not been started. Call to Next() is missing.");
                if (this.currentEntry == null)
                {
                    this.currentEntry = new ArrayBasedDataRecord(this.lineData, this.headerMap);
                }

                return this.currentEntry[fieldName];
            }

        }

        public override string this[int fieldIndex]
        {
            get {
                if (this.lineData == null) throw new InvalidOperationException("Reading from the first entry has not been started. Call to Next() is missing.");
                if (this.currentEntry == null)
                {
                    this.currentEntry = new ArrayBasedDataRecord(this.lineData, this.headerMap);
                }

                return this.currentEntry[fieldIndex];
            }
        }

        public override bool Next()
        {
            string lineText = this.reader.ReadLine();
            if (lineText == null)
            {
                return false;
            }

            this.lineData = lineText.Split(new[] { this.delimiter }, StringSplitOptions.None);
            this.currentEntry = null;
            return true;
        }

        public void Dispose()
        {
            if (this.streamOwner)
            {
                this.reader.Close();
            }
        }

        public IEnumerable<string> Headers
        {
            get
            {
                return from header in this.headerMap
                       orderby header.Value
                       select header.Key;
            }
        }

        protected override IDataRecord GetDataEntry()
        {
            return new ArrayBasedDataRecord(this.lineData, this.headerMap);
        }
    }
}
