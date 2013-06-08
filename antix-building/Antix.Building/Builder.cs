using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public sealed class Builder<T>
        : BuilderBase<Builder<T>, T>
    {
        public Builder()
        {
        }

        public Builder(Func<T> create) : base(create)
        {
        }
    }
}