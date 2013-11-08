using System;
using System.Collections.Generic;

namespace Antix.Building.Abstraction
{
    public interface IBuilder<out T, out TBuildArgs> :
        IEnumerable<T>, ICloneable
    {
        IBuilder<T, TBuildArgs> With(Action<T, TBuildArgs> assign);
        IBuilder<T, TBuildArgs> Validate(Action<T, TBuildArgs> action);

        IBuilder<T, TBuildArgs> Arguments(Action<TBuildArgs> action);

        T Build(Action<T, TBuildArgs> assign);
        IBuilder<T, TBuildArgs> Build(int exactCount, Action<T, TBuildArgs> assign);
    }

    //public interface IBuilder<out T> : IBuilder<T, BuildArgs>
    //{

    //}
}