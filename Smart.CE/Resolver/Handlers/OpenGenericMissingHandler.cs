namespace Smart.Resolver.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.ComponentModel;
    using Smart.Resolver.Bindings;
    using Smart.Resolver.Providers;

    /// <summary>
    ///
    /// </summary>
    public class OpenGenericMissingHandler : IMissingHandler
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="components"></param>
        /// <param name="table"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<IBinding> Handle(IComponentContainer components, IBindingTable table, Type type)
        {
            if (!type.IsGenericType)
            {
                return Enumerable.Empty<IBinding>();
            }

            return table.FindBindings(type.GetGenericTypeDefinition())
                .Select(b => (IBinding)new Binding(
                    type,
                    new StandardProvider(b.Provider.TargetType.MakeGenericType(type.GetGenericArguments()), components),
                    b.Scope,
                    b.Metadata,
                    b.ConstructorArguments,
                    b.PropertyValues));
        }
    }
}
