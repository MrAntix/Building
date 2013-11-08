using Antix.Building.Tests.Pocos;
using Xunit;

namespace Antix.Building.Tests
{
    public class use_properties
    {
        [Fact]
        public void properties_are_cloned()
        {
            const string namePrefix = "PREFIX";
            var builder = new Builder<Thingy>()
                .Arguments(a => a.Properties.prefix = namePrefix)
                .With(o => o.Name = "XX")
                .Validate((o, args) => { o.Name = string.Concat(args.Properties.prefix, o.Name); });

            var thingy = builder.Build();
            Assert.True(thingy.Name.StartsWith(namePrefix));
        }

        [Fact]
        public void properties_can_be_overwitten()
        {
            const string name = "NAME";
            const string namePrefix = "PREFIX";
            var builder = new Builder<Thingy>()
                .Arguments(a => a.Properties.prefix = null)
                .Validate((o, args) => { o.Name = string.Concat(args.Properties.prefix, o.Name); })
                .With(t => t.Name = name);

            var prefixBuilder = builder
                .Arguments(a => a.Properties.prefix = namePrefix);

            var thingy = builder.Build();
            Assert.False(thingy.Name.StartsWith(namePrefix));
            Assert.True(thingy.Name.EndsWith(name));

            thingy = prefixBuilder.Build();
            Assert.True(thingy.Name.StartsWith(namePrefix));
            Assert.True(thingy.Name.EndsWith(name));
        }
    }
}