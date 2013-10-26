namespace Antix.Building
{
    public class BuildArgs
    {
        readonly int _index;
        readonly dynamic _properties;

        public BuildArgs(int index, dynamic properties)
        {
            _index = index;
            _properties = properties;
        }

        public int Index
        {
            get { return _index; }
        }

        public dynamic Properties
        {
            get { return _properties; }
        }
    }
}