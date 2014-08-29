namespace MyData
{
    public class FixedWidthColumn
    {
        private readonly string name;
        private readonly int width;

        public FixedWidthColumn(string name, int width)
        {
            this.name = name;
            this.width = width;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Width
        {
            get { return this.width; }
        }
    }
}
