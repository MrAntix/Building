using Antix.Building.Tests.Pocos;

using Moq;

namespace Antix.Building.Tests
{
    public class create_pocis_using_moq :
        create<IThingy>
    {
        public create_pocis_using_moq() :
            base(new Builder<IThingy>(p => Mock.Of<IThingy>()))
        {
        }
    }
}