using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public static class BuilderExtensions
    {
        public static IBuilder<T> Validate<T>(
            this IBuilder<T> builder,
            Action<T> action)
        {
            return builder.Validate((o, a) => action(o));
        }

        public static IBuilder<T> With<T>(
            this IBuilder<T> builder,
            Action<T> action)
        {
            return builder.With((o, a) => action(o));
        }

        public static T Build<T>(
            this IBuilder<T> builder)
        {
            return builder.Build(null);
        }

        public static T Build<T>(
            this IBuilder<T> builder,
            Action<T> assign)
        {
            return builder.Build((o, a) => assign(o));
        }

        public static IBuilder<T> Build<T>(
            this IBuilder<T> builder,
            int exactCount)
        {
            return builder.Build(exactCount, default(Action<T, BuildArgs>));
        }

        public static IBuilder<T> Build<T>(
            this IBuilder<T> builder,
            int exactCount,
            Action<T> assign)
        {
            return builder.Build(exactCount, (o, a) => assign(o));
        }

        public static IBuilder<T> Apply<T>(
            this IBuilder<T> builder,
            Build<IBuilder<T>> build)
        {
            return build == null ? builder : build(builder);
        }
    }
}