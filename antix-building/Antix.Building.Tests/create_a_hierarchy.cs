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
                .WithThingy(t =>
                            t.WithName(expectedChildName)
                             .WithCount(1))
                .WithThingy(t =>
                            t.WithName("Other")
                             .WithCount(2))
                .WithThingy(t =>
                            t.WithName("And an other")
                             .WithCount(3))
                .Build();

            Assert.Equal(expectedParentName, result.Name);
            Assert.Equal(3, result.Thingies.Count());
            Assert.Equal(expectedChildName, result.Thingies.First().Name);
        }
    }
}