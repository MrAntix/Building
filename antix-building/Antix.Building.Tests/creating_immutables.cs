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
                .Properties(p => p.name = expectedName);

            var result = builder.Build();

            Assert.NotNull(result);
            Assert.Equal(expectedName, result.Name);
        }
    }
}