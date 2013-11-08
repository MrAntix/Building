using System.Linq;
using Antix.Building.Tests.Pocos;
using Xunit;

namespace Antix.Building.Tests
{
    public class use_index
    {
        [Fact]
        public void index_in_same_call()
        {
            var builder = new Builder<Thingy>()
                .With((t, args) => t.Count = args.Index + 1);

            var result = builder.Build(2).ToArray();

            Assert.Equal(1, result.ElementAt(0).Count);
            Assert.Equal(2, result.ElementAt(1).Count);
        }

        [Fact]
        public void index_retained_between_builds()
        {
            var builder = new Builder<Thingy>()
                .With((t, args) => t.Count = args.Index + 1);

            Assert.Equal(1, builder.Build().Count);
            Assert.Equal(2, builder.Build().Count);
        }

        [Fact]
        public void can_reset_index()
        {
            var builder = new Builder<Thingy>()
                .With((t, args) => t.Count = args.Index + 1);

            Assert.Equal(1, builder.Build().Count);

            builder = builder.Index(0);

            Assert.Equal(1, builder.Build().Count);
        }
    }
}