using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public static class BuilderExtensions
    {
        public static IBuilder<T, TBuildArgs> Validate<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder,
            Action<T> action)
        {
            return builder.Validate((o, a) => action(o));
        }

        public static IBuilder<T, TBuildArgs> With<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder,
            Action<T> action)
        {
            return builder.With((o, a) => action(o));
        }

        public static T Build<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder)
        {
            return builder.Build(null);
        }

        public static T Build<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder,
            Action<T> assign)
        {
            return builder.Build((o, a) => assign(o));
        }

        public static IBuilder<T, TBuildArgs> Build<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder,
            int exactCount)
        {
            return builder.Build(exactCount, default(Action<T, TBuildArgs>));
        }

        public static IBuilder<T, TBuildArgs> Build<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder,
            int exactCount,
            Action<T> assign)
        {
            return builder.Build(exactCount, (o, a) => assign(o));
        }

        public static IBuilder<T, TBuildArgs> Apply<T, TBuildArgs>(
            this IBuilder<T, TBuildArgs> builder,
            Build<IBuilder<T, TBuildArgs>> build)
        {
            return build == null ? builder : build(builder);
        }
    }
}