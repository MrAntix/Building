//  by Anthony J. Johnston, antix.co.uk

using System.Linq;

using Antix.Building.Tests.Immutables;

using Xunit;

namespace Antix.Building.Tests
{
    public class creating_immutables
    {
        [Fact]
        public void create_one()
        {
            const string expectedName = "NAME";

            var builder = new Builder<Thingy>(
                args => new Thingy(args.Properties.name, 0))
                .Arguments(a => a.Properties.name = expectedName);

            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(expectedName, result.Name);
        }

        [Fact]
        public void non_dynamic_properties()
        {
            const string expectedName = "NAME";
            var builder = new Builder<Thingy, BuildArgs<ThingyProperties>>()
                .Create(a => new Thingy(a.Properties.Name, a.Index + 1))
                .Arguments(a => a.Properties.Name = expectedName);

            var result = builder.Build(10).Last();

            Assert.Equal(expectedName, result.Name);
            Assert.Equal(10, result.Count);
        }
    }
}