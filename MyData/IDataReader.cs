using System.Collections.Generic;

namespace MyData
{
    public interface IDataReader : IEnumerable<IDataRecord>
    {
        bool Next();

        string this[int fieldIndex] { get; }

        string this[string fieldName] { get; }
    }
}
