using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public static class BuilderExtensions
    {
        public static T Build<T>(
            this IBuilder<T> builder)
        {
            return builder.Build(null);
        }

        public static IBuilder<T> Build<T>(
            this IBuilder<T> builder,
            int exactCount)
        {
            return builder.Build(exactCount, default(Action<T, int>));
        }

        public static TBuilder Apply<TBuilder>(
            this TBuilder builder,
            Build<TBuilder> build)
        {
            return build == null ? builder : build(builder);
        }
    }
}