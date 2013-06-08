using Antix.Building.Tests.Pocos;

namespace Antix.Building.Tests
{
    public class create_pocos :
        create<Thingy>
    {
        public create_pocos() :
            base(new Builder<Thingy>())
        {
        }
    }
}