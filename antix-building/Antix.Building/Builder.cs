using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public sealed class Builder<T>
        : BuilderBase<Builder<T>, T>
    {
        public Builder(Func<BuildArgs, T> create)
            : base(create)
        {
        }

        public Builder(Func<T> create)
            : this(p => create())
        {
        }

        public Builder()
            : this(default(Func<BuildArgs, T>))
        {
        }
    }
}