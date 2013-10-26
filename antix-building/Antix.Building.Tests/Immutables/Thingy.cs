namespace Antix.Building.Tests.Immutables
{
    public class Thingy
    {
        readonly string _name;
        readonly int _count;

        public Thingy(string name, int count)
        {
            _name = name;
            _count = count;
        }

        public string Name
        {
            get { return _name; }
        }

        public int Count
        {
            get { return _count; }
        }
    }
}