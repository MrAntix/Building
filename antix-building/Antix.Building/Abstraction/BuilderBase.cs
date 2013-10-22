using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Antix.Building.Abstraction
{
    public abstract class BuilderBase<TBuilder, T> :
        IBuilder<T>
        where TBuilder : class, IBuilder<T>
    {
        protected Action<T> Assign;
        Func<T> _create;
        Action<T> _validate;

        protected BuilderBase(
            Func<T> create, Action<T> validate)
        {
            _create = create ?? Activator.CreateInstance<T>;
            _validate = validate;
        }

        protected BuilderBase()
            : this(null, null)
        {
        }

        protected BuilderBase(Func<T> create)
            : this(create, null)
        {
        }

        protected BuilderBase(Action<T> validate)
            : this(null, validate)
        {
        }

        protected IEnumerable<T> Items { get; set; }

        public int Index { get; protected set; }

        IBuilder<T> IBuilder<T>.ResetIndex()
        {
            return ResetIndex();
        }

        public BuilderBase<TBuilder, T> ResetIndex()
        {
            Index = 0;
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

            // copy other properties
            clone.Assign = Assign;
            clone.Index = Index;
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

        IBuilder<T> IBuilder<T>.With(Action<T> assign)
        {
            return With(assign);
        }

        public TBuilder With(
            Action<T> assign)
        {
            if (assign == null) throw new ArgumentNullException("assign");

            var clone = ClonePrivate();
            clone.Assign = Assign == null
                               ? assign
                               : x =>
                                   {
                                       Assign(x);
                                       assign(x);
                                   };

            return clone as TBuilder;
        }

        public T Build(Action<T> assign)
        {
            var item = Create();

            if (Assign != null) Assign(item);
            if (assign != null) assign(item);

            Validate(item);

            return item;
        }

        protected virtual T Create()
        {
            try
            {
                return _create();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format("Cannot create '{0}'", typeof (T).FullName),
                    ex);
            }
        }

        protected virtual void Validate(T item)
        {
            if (_validate != null) _validate(item);
        }

        IBuilder<T> IBuilder<T>.Build(int exactCount, Action<T> assign)
        {
            return Build(exactCount, assign);
        }

        IBuilder<T> IBuilder<T>.Build(int exactCount, Action<T, int> assign)
        {
            return Build(exactCount, assign);
        }

        public TBuilder Build(
            int exactCount,
            Action<T> assign)
        {
            return Build(exactCount,
                         assign == null
                             ? default(Action<T, int>)
                             : (x, i) => assign(x));
        }

        public TBuilder Build(
            int exactCount,
            Action<T, int> assign)
        {
            var items =
                Enumerable.Range(0, exactCount)
                          .Select(index =>
                              {
                                  var item = Build(assign == null
                                                       ? null
                                                       : (Action<T>) (o => assign(o, Index + index))
                                      );

                                  return item;
                              });

            if (Items != null) items = Items.Concat(items);

            var clone = ClonePrivate();
            clone.Index = exactCount;
            clone.Items = items;

            return clone as TBuilder;
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