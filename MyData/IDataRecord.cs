namespace MyData
{
    public interface IDataRecord
    {
        string this[int fieldIndex]
        {
            get;
        }

        string this[string fieldName]
        {
            get;
        }

        bool HasField(string fieldName);
    }
}
