using System;
using System.Collections.Generic;

namespace Antix.Building.Abstraction
{
    public interface IBuilder<out T> :
        IEnumerable<T>, ICloneable
    {
        IBuilder<T> With(Action<T, BuildArgs> assign);
        IBuilder<T> Validate(Action<T, BuildArgs> action);

        IBuilder<T> Properties(Action<dynamic> action);
        IBuilder<T> Index(int value);

        T Build(Action<T, BuildArgs> assign);
        IBuilder<T> Build(int exactCount, Action<T, BuildArgs> assign);
    }
}