using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace Antix.Building.Abstraction
{
    public abstract class BuilderBase<TBuilder, T> :
        IBuilder<T>
        where TBuilder : class, IBuilder<T>
    {
        protected Action<T, BuildArgs> Assign;
        Func<BuildArgs, T> _create;
        Action<T, BuildArgs> _validate;

        int _index;
        dynamic _properties;

        protected BuilderBase(Func<BuildArgs, T> create)
        {
            _create = create ?? (p => Activator.CreateInstance<T>());
        }

        protected BuilderBase(Func<T> create)
            : this(p => create())
        {
        }

        protected BuilderBase()
            : this(default(Func<BuildArgs, T>))
        {
        }

        protected IEnumerable<T> Items { get; set; }

        IBuilder<T> IBuilder<T>.Index(int value)
        {
            return Index(value);
        }

        public BuilderBase<TBuilder, T> Index(int value)
        {
            _index = value;
            return this;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public TBuilder Clone()
        {
            return ClonePrivate() as TBuilder;
        }

        BuilderBase<TBuilder, T> ClonePrivate()
        {
            var clone = CreateClone()
                        as BuilderBase<TBuilder, T>;
            Debug.Assert(clone != null, "clone != null");

            // set here, don't want inheritors to have to deal with these
            clone._create = _create;
            clone._validate = _validate;

            var newProperties = new ExpandoObject()
                                as IDictionary<string, object>;
            if (_properties != null)
            {
                foreach (var kv in (IDictionary<string, object>) _properties)
                    newProperties[kv.Key] = kv.Value;
            }

            clone._properties = newProperties;

            // copy other properties
            clone.Assign = Assign;
            clone._index = _index;
            clone.Items = Items;

            return clone;
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
                return _create(new BuildArgs(_index, _properties));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format("Cannot create '{0}'", typeof (T).FullName),
                    ex);
            }
        }

        IBuilder<T> IBuilder<T>.Properties(Action<dynamic> action)
        {
            return Properties(action);
        }

        public TBuilder Properties(Action<dynamic> action)
        {
            var clone = ClonePrivate();
            action(clone._properties);

            return clone as TBuilder;
        }

        protected virtual void Validate(T item, BuildArgs args)
        {
            if (_validate != null) _validate(item, args);
        }

        public TBuilder Validate(Action<T, BuildArgs> action)
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

        IBuilder<T> IBuilder<T>.Validate(Action<T, BuildArgs> action)
        {
            return Validate(action);
        }

        public TBuilder With(
            Action<T, BuildArgs> assign)
        {
            if (assign == null) throw new ArgumentNullException("assign");

            var clone = ClonePrivate();
            clone.Assign = Assign == null
                               ? assign
                               : (o, i) =>
                                   {
                                       Assign(o, i);
                                       assign(o, i);
                                   };

            return clone as TBuilder;
        }

        public TBuilder With(Action<T> action)
        {
            return With((o, a) => action(o));
        }

        IBuilder<T> IBuilder<T>.With(Action<T, BuildArgs> assign)
        {
            return With(assign);
        }

        public T Build(Action<T, BuildArgs> assign)
        {
            var item = Create();

            var args = new BuildArgs(_index++, _properties);

            if (Assign != null) Assign(item, args);
            if (assign != null) assign(item, args);

            Validate(item, args);

            return item;
        }

        public T Build(Action<T> assign)
        {
            return Build((o, a) => assign(o));
        }

        public TBuilder Build(
            int exactCount,
            Action<T, BuildArgs> assign)
        {
            var items =
                Enumerable.Range(0, exactCount)
                          .Select(_ => Build(assign));

            if (Items != null) items = Items.Concat(items);

            var clone = ClonePrivate();
            clone._index = exactCount;
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
            return Build(exactCount, default(Action<T, BuildArgs>));
        }

        IBuilder<T> IBuilder<T>.Build(
            int exactCount, Action<T, BuildArgs> assign)
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
}