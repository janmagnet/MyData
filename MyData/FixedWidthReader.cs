using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyData
{
    public class FixedWidthReader : DataReader, IDisposable
    {
        private readonly StreamReader reader;
        private readonly int[] columnWidths;
        private readonly bool streamOwner;
        private string[] lineData;
        private IDictionary<string, int> headerMap;
        private IDataRecord currentEntry;

        public FixedWidthReader(StreamReader reader, FixedWidthColumn[] columns)
        {
            this.reader = reader;
            this.streamOwner = false;
            this.headerMap = new Dictionary<string, int>();
            this.columnWidths = new int[columns.Length];

            this.SetupColumns(columns);
        }

        public FixedWidthReader(StreamReader reader, int[] columnWidths)
        {
            this.reader = reader;
            this.streamOwner = false;
            this.headerMap = new Dictionary<string, int>();
            this.columnWidths = columnWidths;
        }

        public FixedWidthReader(string fileName, int[] columnWidths)
            : this(fileName, columnWidths, Encoding.UTF8)
        {
        }

        public FixedWidthReader(string fileName, int[] columnWidths, Encoding encoding)
        {
            this.reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), encoding);
            this.streamOwner = true;
            this.headerMap = new Dictionary<string, int>();
            columnWidths = new int[columnWidths.Length];
        }

        public FixedWidthReader(string fileName, FixedWidthColumn[] columns)
            : this(fileName, columns, Encoding.UTF8)
        {
        }

        public FixedWidthReader(string fileName, FixedWidthColumn[] columns, Encoding encoding)
        {
            this.reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), encoding);
            this.streamOwner = true;
            this.headerMap = new Dictionary<string, int>();
            this.columnWidths = new int[columns.Length];

            this.SetupColumns(columns);
        }

        private void SetupColumns(FixedWidthColumn[] columns)
        {
            for (int i = 0; i < columns.Length; i++)
            {
                FixedWidthColumn column = columns[i];
                this.columnWidths[i] = column.Width;
                this.headerMap.Add(column.Name, i);
            }
        }

        private void ReadHeaders()
        {
            this.headerMap = new Dictionary<string, int>();		
		
            string headerLine = this.reader.ReadLine();
            string[] headers = this.SplitLine(headerLine);
            for (int i = 0; i < headers.Length; i++) {
                this.headerMap.Add(headers[i], i);
            }
        }

        private string[] SplitLine(string lineText)
        {
            int start = 0;
            string[] lineData = new string[this.columnWidths.Length];
            for (int i = 0; i < this.columnWidths.Length; i++)
            {
                int width = this.columnWidths[i];
                lineData[i] = lineText.Substring(start, width);
                start += width;
            }

            return lineData;
        }

        public override string this[string fieldName]
        {
            get
            {
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
            get
            {
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

            this.lineData = this.SplitLine(lineText);
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
