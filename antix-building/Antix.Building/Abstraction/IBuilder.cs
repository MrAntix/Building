using System;
using System.Collections.Generic;

namespace Antix.Building.Abstraction
{
    public interface IBuilder<out TBuilder, out T> :
        IEnumerable<T>, ICloneable
        where TBuilder : class, IBuilder<TBuilder, T>
    {
        TBuilder With(Action<T> assign);

        T Build(Action<T> assign);

        TBuilder Build(int exactCount, Action<T> assign);
        TBuilder Build(int exactCount, Action<T, int> assign);

        int Index { get; }
        IBuilder<TBuilder, T> ResetIndex();
    }
}