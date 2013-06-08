using System;
using System.Linq;
using Antix.Building.Abstraction;

namespace Antix.Building.Tests.Pocos
{
    public static class ThingyExtensions
    {
        public static TBuilder WithName<TBuilder>(
            this TBuilder builder, string value)
            where TBuilder : class, IBuilder<IThingyHasName>
        {
            return (TBuilder) builder.With(a => a.Name = value);
        }

        public static TBuilder WithThingy<TBuilder>(
            this TBuilder builder,
            Build<IBuilder<Thingy>> buildThingy)
            where TBuilder : class, IBuilder<ThingyParent>
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (buildThingy == null) throw new ArgumentNullException("buildThingy");

            var newThingy
                = new Builder<Thingy>()
                    .Apply(buildThingy)
                    .Build(1);

            return (TBuilder) builder.With(
                p =>
                    {
                        p.Thingies = (
                                         p.Thingies == null
                                             ? newThingy
                                             : p.Thingies.Concat(newThingy)
                                     ).ToArray();
                    });
        }

        public static TBuilder WithCount<TBuilder>(
            this TBuilder builder, int value)
            where TBuilder : class, IBuilder<Thingy>
        {
            return (TBuilder) builder.With(a => a.Count = value);
        }
    }
}