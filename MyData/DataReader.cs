using System.Collections.Generic;

namespace MyData
{
    public abstract class DataReader : IDataReader
    {
        protected abstract IDataRecord GetDataEntry();

        public abstract bool Next();

        public abstract string this[int fieldIndex] { get; }

        public abstract string this[string fieldName] { get; }

        public IEnumerator<IDataRecord> GetEnumerator()
        {
            while (this.Next())
            {
                yield return this.GetDataEntry();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
