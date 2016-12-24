namespace Smart.Resolver.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// <param name="table"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public IEnumerable<IBinding> Handle(IBindingTable table, Type type)
        {
            if (!type.IsGenericType)
            {
                return Enumerable.Empty<IBinding>();
            }

            return table.FindBindings(type.GetGenericTypeDefinition())
                .Select(b => (IBinding)new Binding(
                    type,
                    new StandardProvider(b.Provider.TargetType.MakeGenericType(type.GetGenericArguments())),
                    b.Scope,
                    b.Metadata,
                    b.ConstructorArguments,
                    b.PropertyValues));
        }
    }
}
