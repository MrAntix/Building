using Antix.Building.Abstraction;
using Xunit;

namespace Antix.Building.Tests
{
    public abstract class create<T>
    {
        readonly IBuilder<T, dynamic> _builder;

        protected create(
            IBuilder<T, dynamic> builder)
        {
            _builder = builder;
        }

        [Fact]
        public void creates_instance()
        {
            var item = _builder.Build();

            Assert.NotNull(item);
        }
    }
}