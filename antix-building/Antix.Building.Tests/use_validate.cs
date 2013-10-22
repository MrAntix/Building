using System.Linq;
using Antix.Building.Tests.Pocos;
using Xunit;

namespace Antix.Building.Tests
{
    public class use_validate
    {
        const string ExpectedName = "Expected";
        const int ExpectedCount = 99;

        const string NotExpectedName = "NotExpected";

        [Fact]
        public void validate_is_called_after_all_withs()
        {
            var builder = new Builder<Thingy>(t => t.Name = ExpectedName)
                .With(t => t.Name = NotExpectedName);

            var result = builder.Build();

            Assert.Equal(ExpectedName, result.Name);
        }

        [Fact]
        public void validate_is_called_after_build_with()
        {
            var builder = new Builder<Thingy>(t => t.Name = ExpectedName);

            var result = builder.Build(i => i.Name = NotExpectedName);

            Assert.Equal(ExpectedName, result.Name);
        }

        [Fact]
        public void validate_is_called_after_build_with_many()
        {
            var builder = new Builder<Thingy>(t => t.Name = ExpectedName);

            var result = builder.Build(10, i => i.Name = NotExpectedName).First();

            Assert.Equal(ExpectedName, result.Name);
        }
    }
}