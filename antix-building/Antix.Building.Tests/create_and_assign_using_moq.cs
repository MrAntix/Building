using System.Linq;
using Antix.Building.Abstraction;
using Antix.Building.Tests.Pocos;

using Moq;
using Xunit;

namespace Antix.Building.Tests
{
    public class create_and_assign_using_moq
    {
        readonly IBuilder<IThingy, dynamic> _builder;
        readonly string _expectedName;
        readonly string[] _names = new[] {"Abe", "Bob", "Charlie", "Derric"};

        public create_and_assign_using_moq()
        {
            _expectedName = _names.First();
            _builder = new Builder<IThingy>(Mock.Of<IThingy>)
                        .With(x => x.Name = _expectedName);
        }

        [Fact]
        public void can_assign_values()
        {
            var item = _builder.Build();
            Assert.Equal(_expectedName, item.Name);
        }

        [Fact]
        public void can_override_assigned_values()
        {
            var overridenName = _names.ElementAt(1);

            var item = _builder
                .Build(x => x.Name = overridenName);

            Assert.Equal(overridenName, item.Name);
        }
    }
}