using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public static class BuilderExtensions
    {
        public static T Build<TBuilder, T>(
            this IBuilder<TBuilder, T> builder)
            where TBuilder : class, IBuilder<TBuilder, T>
        {
            return builder.Build(null);
        }

        public static TBuilder Build<TBuilder, T>(
            this IBuilder<TBuilder, T> builder,
            int exactCount)
            where TBuilder : class, IBuilder<TBuilder, T>
        {
            return builder.Build(exactCount, default(Action<T, int>));
        }
    }
}