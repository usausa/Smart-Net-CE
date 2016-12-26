namespace Smart.Resolver.Metadatas
{
    using System.Collections.Generic;
    using System.Reflection;

    using Smart.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class ActivatorCache
    {
        private static readonly Dictionary<ConstructorInfo, IActivator> Activators = new Dictionary<ConstructorInfo, IActivator>();

        public static IActivator GetActivator(ConstructorInfo ci)
        {
            lock (Activators)
            {
                IActivator activator;
                if (!Activators.TryGetValue(ci, out activator))
                {
                    activator = ci.ToActivator();
                    Activators[ci] = activator;
                }

                return activator;
            }
        }
    }
}
