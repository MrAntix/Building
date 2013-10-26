using System.Collections.Generic;

namespace Antix.Building.Tests.Immutables
{
    public class ThingyParent
    {
        readonly string _name;
        readonly IEnumerable<Thingy> _thingies;

        public ThingyParent(string name, IEnumerable<Thingy> thingies)
        {
            _name = name;
            _thingies = thingies;
        }

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<Thingy> Thingies
        {
            get { return _thingies; }
        }
    }
}