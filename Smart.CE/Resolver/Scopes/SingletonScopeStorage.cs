namespace Smart.Resolver.Scopes
{
    using System;
    using System.Collections.Generic;

    using Smart.ComponentModel;

    using Smart.Resolver.Bindings;

    /// <summary>
    ///
    /// </summary>
    public class SingletonScopeStorage : DisposableObject, ISingletonScopeStorage
    {
        private readonly Dictionary<IBinding, object> cache = new Dictionary<IBinding, object>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Clear();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="instance"></param>
        public void Remember(IBinding binding, object instance)
        {
            lock (cache)
            {
                cache[binding] = instance;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        public object TryGet(IBinding binding)
        {
            lock (cache)
            {
                object instance;
                return cache.TryGetValue(binding, out instance) ? instance : null;
            }
        }

        /// <summary>
        ///
        /// </summary>
        public void Clear()
        {
            lock (cache)
            {
                foreach (var instance in cache.Values)
                {
                    var disposable = instance as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                cache.Clear();
            }
        }
    }
}
