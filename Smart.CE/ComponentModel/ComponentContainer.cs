namespace Smart.ComponentModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class ComponentContainer : DisposableObject, IComponentContainer
    {
        private readonly Dictionary<Type, object> components = new Dictionary<Type, object>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var instance in components.Values)
                {
                    var disposable = instance as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }

                components.Clear();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public void Register<T>(T component)
        {
            components[typeof(T)] = component;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>()
        {
            components.Remove(typeof(T));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            object value;
            return (T)(components.TryGetValue(typeof(T), out value) ? value : null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Get(Type type)
        {
            object value;
            return components.TryGetValue(type, out value) ? value : null;
        }
    }
}
