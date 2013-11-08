//  by Anthony J. Johnston, antix.co.uk

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Antix.Building.Abstraction
{
    public abstract class BuilderBase<TBuilder, T, TBuildArgs> :
        IBuilder<T, TBuildArgs>
        where TBuilder : class, IBuilder<T, TBuildArgs>
        where TBuildArgs : IBuildArgs, new()
    {
        Action<T, TBuildArgs> _assign;
        Func<TBuildArgs, T> _create;
        Action<T, TBuildArgs> _validate;

        TBuildArgs _buildArgs;

        protected BuilderBase(Func<TBuildArgs, T> create)
        {
            _create = create ?? (p => Activator.CreateInstance<T>());
            _buildArgs = new TBuildArgs();
        }

        protected BuilderBase(Func<T> create)
            : this(p => create())
        {
        }

        protected BuilderBase()
            : this(default(Func<TBuildArgs, T>))
        {
        }

        protected IEnumerable<T> Items { get; set; }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public TBuilder Clone()
        {
            return ClonePrivate() as TBuilder;
        }

        BuilderBase<TBuilder, T, TBuildArgs> ClonePrivate(IBuildArgs buildArgs)
        {
            var clone = CreateClone()
                        as BuilderBase<TBuilder, T, TBuildArgs>;
            Debug.Assert(clone != null, "clone != null");

            // set here, don't want inheritors to have to deal with these
            clone._create = _create;
            clone._validate = _validate;
            clone._buildArgs = (TBuildArgs) buildArgs;

            // copy other properties
            clone._assign = _assign;
            clone.Items = Items;

            return clone;
        }

        BuilderBase<TBuilder, T, TBuildArgs> ClonePrivate()
        {
            return ClonePrivate(
                _buildArgs.Create(_buildArgs.Index, _buildArgs.Properties));
        }

        protected virtual TBuilder CreateClone()
        {
            try
            {
                return Activator.CreateInstance<TBuilder>();
            }
            catch (MissingMethodException mmex)
            {
                throw new MissingMethodException(
                    string.Format("Override the CreateClone method on '{0}', no default contructor found",
                                  typeof (TBuilder).FullName),
                    mmex);
            }
        }

        protected virtual T Create()
        {
            try
            {
                return _create(_buildArgs);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format("Cannot create '{0}'", typeof (T).FullName),
                    ex);
            }
        }

        public TBuilder Create(Func<TBuildArgs, T> action)
        {
            var clone = ClonePrivate();
            clone._create = action;

            return clone as TBuilder;
        }

        public TBuilder Index(int value)
        {
            return ClonePrivate(
                _buildArgs.Create(value, _buildArgs.Properties))
                   as TBuilder;
        }

        public TBuilder Arguments(Action<TBuildArgs> action)
        {
            var clone = ClonePrivate();
            action(clone._buildArgs);

            return clone as TBuilder;
        }

        IBuilder<T, TBuildArgs> IBuilder<T, TBuildArgs>
            .Arguments(Action<TBuildArgs> action)
        {
            return Arguments(action);
        }

        protected virtual void Validate(T item, TBuildArgs args)
        {
            if (_validate != null) _validate(item, args);
        }

        public TBuilder Validate(Action<T, TBuildArgs> action)
        {
            if (action == null) throw new ArgumentNullException("action");

            var clone = ClonePrivate();
            clone._validate = action;

            return clone as TBuilder;
        }

        public TBuilder Validate(Action<T> action)
        {
            return Validate((o, a) => action(o));
        }

        IBuilder<T, TBuildArgs> IBuilder<T, TBuildArgs>.Validate(Action<T, TBuildArgs> action)
        {
            return Validate(action);
        }

        public TBuilder With(
            Action<T, TBuildArgs> assign)
        {
            if (assign == null) throw new ArgumentNullException("assign");

            var clone = ClonePrivate();
            clone._assign = _assign == null
                                ? assign
                                : (o, i) =>
                                    {
                                        _assign(o, i);
                                        assign(o, i);
                                    };

            return clone as TBuilder;
        }

        public TBuilder With(Action<T> action)
        {
            return With((o, a) => action(o));
        }

        IBuilder<T, TBuildArgs> IBuilder<T, TBuildArgs>.With(Action<T, TBuildArgs> assign)
        {
            return With(assign);
        }

        public T Build(Action<T, TBuildArgs> assign)
        {
            var item = Create();

            if (_assign != null) _assign(item, _buildArgs);
            if (assign != null) assign(item, _buildArgs);

            Validate(item, _buildArgs);
            _buildArgs.Index++;

            return item;
        }

        public T Build(Action<T> assign)
        {
            return Build((o, a) => assign(o));
        }

        public TBuilder Build(
            int exactCount,
            Action<T, TBuildArgs> assign)
        {
            var items =
                Enumerable.Range(0, exactCount)
                          .Select(_ => Build(assign));

            if (Items != null) items = Items.Concat(items);

            var clone = ClonePrivate(
                _buildArgs.Create(_buildArgs.Index + exactCount, _buildArgs.Properties));

            clone.Items = items;

            return clone as TBuilder;
        }

        public TBuilder Build(
            int exactCount,
            Action<T> assign)
        {
            return Build(exactCount, (o, a) => assign(o));
        }

        public TBuilder Build(
            int exactCount)
        {
            return Build(exactCount, default(Action<T, TBuildArgs>));
        }

        IBuilder<T, TBuildArgs> IBuilder<T, TBuildArgs>.Build(
            int exactCount, Action<T, TBuildArgs> assign)
        {
            return Build(exactCount, assign);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class BuilderBase<TBuilder, T>
        : BuilderBase<TBuilder, T, BuildArgs>
        where TBuilder : class, IBuilder<T, BuildArgs>
    {
        protected BuilderBase(Func<BuildArgs, T> create)
            : base(create)
        {
        }

        protected BuilderBase(Func<T> create)
            : this(p => create())
        {
        }

        protected BuilderBase()
            : this(default(Func<BuildArgs, T>))
        {
        }
    }
}