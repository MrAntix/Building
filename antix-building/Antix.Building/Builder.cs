using System;
using Antix.Building.Abstraction;

namespace Antix.Building
{
    public sealed class Builder<T, TBuildArgs>
        : BuilderBase<Builder<T, TBuildArgs>, T, TBuildArgs>
        where TBuildArgs : IBuildArgs, new()
    {
        public Builder(Func<TBuildArgs, T> create)
            : base(create)
        {
        }

        public Builder(Func<T> create)
            : this(p => create())
        {
        }

        public Builder()
            : this(default(Func<TBuildArgs, T>))
        {
        }
    }

    public sealed class Builder<T>
        : BuilderBase<Builder<T>, T, BuildArgs>
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