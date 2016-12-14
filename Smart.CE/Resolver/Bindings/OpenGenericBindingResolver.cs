namespace Smart.Resolver.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Resolver.Providers;

    /// <summary>
    ///
    /// </summary>
    public class OpenGenericBindingResolver : IBindingResolver
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public IEnumerable<IBinding> Resolve(IResolverContext context, Type type)
        {
            if (!type.IsGenericType)
            {
                return Enumerable.Empty<IBinding>();
            }

            return context.FindBindings(type.GetGenericTypeDefinition()).Select(b => (IBinding)new Binding(type, new BindingMetadata())
            {
                Provider = new StandardProvider(b.Provider.TargetType.MakeGenericType(type.GetGenericArguments().Where(t => !t.IsGenericParameter).ToArray()))
            });
        }
    }
}
