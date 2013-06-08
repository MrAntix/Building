using System;
using System.Collections.Generic;

namespace Antix.Building.Abstraction
{
    public interface IBuilder<out T> :
        IEnumerable<T>, ICloneable
    {
        IBuilder<T> With(Action<T> assign);

        T Build(Action<T> assign);
        IBuilder<T> Build(int exactCount, Action<T> assign);
        IBuilder<T> Build(int exactCount, Action<T, int> assign);

        int Index { get; }
        IBuilder<T> ResetIndex();
    }
}