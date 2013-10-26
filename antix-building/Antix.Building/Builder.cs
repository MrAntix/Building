using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public sealed class Builder<T>
        : BuilderBase<Builder<T>, T>, IBuilder<T>
    {
        public Builder(Func<T> create)
            : base(create)
        {
        }

        public Builder()
            : this(null)
        {
        }
    }
}