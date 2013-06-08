using Xunit;

namespace Antix.Building.Tests
{
    public abstract class create<T>
    {
        readonly Builder<T> _builder;

        protected create(
            Builder<T> builder)
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