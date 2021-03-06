//  by Anthony J. Johnston, antix.co.uk

using System;
using System.Linq;

using Antix.Building.Abstraction;

namespace Antix.Building.Tests.Pocos
{
    public static class ThingyExtensions
    {
        public static TBuilder WithName<TBuilder>(
            this TBuilder builder, string value)
            where TBuilder : class, IBuilder<IThingyHasName, BuildArgs>
        {
            return (TBuilder) builder.With(o => o.Name = value);
        }

        public static TBuilder WithThingy<TBuilder>(
            this TBuilder builder,
            Build<IBuilder<Thingy, BuildArgs>> buildThingy)
            where TBuilder : class, IBuilder<ThingyParent, BuildArgs>
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (buildThingy == null) throw new ArgumentNullException("buildThingy");

            var thingyBuilder = new Builder<Thingy>().Apply(buildThingy);
            var newThingy
                = thingyBuilder.Build(1);

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
            where TBuilder : class, IBuilder<Thingy, BuildArgs>
        {
            return (TBuilder) builder.With(a => a.Count = value);
        }
    }
}