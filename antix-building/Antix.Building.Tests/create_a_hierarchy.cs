using System.Linq;
using Antix.Building.Tests.Pocos;
using Xunit;

namespace Antix.Building.Tests
{
    public class create_a_hierarchy
    {
        [Fact]
        public void creates_hierarchy()
        {
            const string expectedParentName = "PARENT";
            const string expectedChildName = "CHILD";

            var builder = new Builder<ThingyParent>();

            var result = builder
                .WithName(expectedParentName)
                .WithThingy(t => t.WithName(expectedChildName))
                .WithThingy(t => t.WithName("Other"))
                .Build();

            Assert.Equal(expectedParentName, result.Name);
            Assert.Equal(2, result.Thingies.Count());
            Assert.Equal(expectedChildName, result.Thingies.First().Name);
        }
    }
}