//  by Anthony J. Johnston, antix.co.uk

using System.Collections.Generic;
using System.Dynamic;

namespace Antix.Building
{
    public class BuildArgs<T> : IBuildArgs where T : new()
    {
        readonly T _properties;

        protected BuildArgs(int index, T properties)
        {
            Index = index;
            _properties = properties;
        }

        public BuildArgs() : this(0, new T())
        {
        }

        public int Index { get; set; }

        public T Properties
        {
            get { return _properties; }
        }

        protected virtual BuildArgs<T> Create(int index, T properties)
        {
            return new BuildArgs<T>(index, properties);
        }

        IBuildArgs IBuildArgs.Create(int index, object properties)
        {
            return Create(index, (T) properties);
        }

        object IBuildArgs.Properties
        {
            get { return Properties; }
        }
    }

    public class BuildArgs : BuildArgs<dynamic>
    {
        public BuildArgs() : base(0, new ExpandoObject())
        {
        }

        BuildArgs(int index, ExpandoObject properties)
            : base(index, properties)
        {
        }

        protected override BuildArgs<dynamic> Create(int index, dynamic properties)
        {
            var newProperties = new ExpandoObject();
            var newPropertiesDictionary = (IDictionary<string, object>) newProperties;
            foreach (var kv in (IDictionary<string, object>) properties)
                newPropertiesDictionary[kv.Key] = kv.Value;

            return new BuildArgs(index, newProperties);
        }
    }
}