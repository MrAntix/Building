using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public sealed class Builder<T>
        : BuilderBase<Builder<T>, T>
    {
        public Builder(Func<T> create, Action<T> validate)
            : base(create, validate)
        {
        }

        public Builder()
            : this(null, null)
        {
        }

        public Builder(Func<T> create)
            : this(create, null)
        {
        }

        public Builder(Action<T> validate)
            : this(null, validate)
        {
        }
    }
}