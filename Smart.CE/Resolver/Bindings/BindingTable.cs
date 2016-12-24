namespace Smart.Resolver.Bindings
{
    using System;
    using System.Collections.Generic;

    public class BindingTable : IBindingTable
    {
        private static readonly IBinding[] EmptyBindings = new IBinding[0];

        private readonly Dictionary<Type, IBinding[]> table = new Dictionary<Type, IBinding[]>();

        public void Add(Type type, IBinding[] bindings)
        {
            lock (table)
            {
                table[type] = bindings;
            }
        }

        public IBinding[] GetOrAdd(Type type, Func<Type, IBinding[]> factory)
        {
            lock (table)
            {
                IBinding[] bindings;
                if (!table.TryGetValue(type, out bindings))
                {
                    bindings = factory(type);
                    table[type] = bindings;
                }

                return bindings;
            }
        }

        public IBinding[] FindBindings(Type type)
        {
            lock (table)
            {
                IBinding[] bindings;
                return table.TryGetValue(type, out bindings) ? bindings : EmptyBindings;
            }
        }
    }
}
