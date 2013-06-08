using System;
using Antix.Building.Abstraction;
using Xunit;

namespace Antix.Building.Tests
{
    public class with_no_default_constructor
    {
        [Fact]
        public void builderBase_clone_throws_exception()
        {
            var ex = Assert.Throws<MissingMethodException>(
                () => { new ABuilder("a").With(x => x.Prop = ""); });

            Console.WriteLine(ex.Message);
        }

        class A
        {
            public string Prop { get; set; }
        }

        class ABuilder : BuilderBase<ABuilder, A>
        {
            // Cannot create this without an explicit create action
            public ABuilder(string value)
            {
            }
        }
    }
}