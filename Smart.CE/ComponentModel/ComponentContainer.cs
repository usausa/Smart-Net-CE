namespace Smart.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public class ComponentContainer : IComponentContainer
    {
        private readonly Dictionary<Type, ICollection<Type>> mapping = new Dictionary<Type, ICollection<Type>>();

        private readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

        /// <summary>
        ///
        /// </summary>
        ~ComponentContainer()
        {
            Dispose(false);
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (instances)
                {
                    foreach (var instance in instances.Values)
                    {
                        var disposable = instance as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                    }

                    instances.Clear();
                }

                mapping.Clear();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public void Add<TComponent, TImplement>()
            where TImplement : TComponent
        {
            var key = typeof(TComponent);

            ICollection<Type> list;
            if (!mapping.TryGetValue(key, out list))
            {
                list = new List<Type>();
                mapping[key] = list;
            }

            list.Add(typeof(TImplement));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        public void RemoveAll<TComponent>()
        {
            RemoveAll(typeof(TComponent));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        public void RemoveAll(Type componentType)
        {
            ICollection<Type> list;
            if (!mapping.TryGetValue(componentType, out list))
            {
                return;
            }

            lock (instances)
            {
                foreach (var implementationType in list)
                {
                    object instance;
                    if (instances.TryGetValue(implementationType, out instance))
                    {
                        var disposable = instance as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }

                        instances.Remove(implementationType);
                    }
                }
            }

            mapping.Remove(componentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>()
        {
            return GetAll(typeof(T)).Cast<T>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public object Get(Type componentType)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException("componentType");
            }

            ICollection<Type> list;
            var implementationType = mapping.TryGetValue(componentType, out list) ? list.FirstOrDefault() : null;
            if (implementationType == null)
            {
                throw new InvalidOperationException(
                    String.Format(CultureInfo.InvariantCulture, "No such component registerd. component type = {0}", componentType.Name));
            }

            return ResolveInstance(implementationType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetAll(Type componentType)
        {
            ICollection<Type> list;
            if (!mapping.TryGetValue(componentType, out list))
            {
                yield break;
            }

            foreach (var implementationType in list)
            {
                yield return ResolveInstance(implementationType);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return Get(serviceType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        private object ResolveInstance(Type implementationType)
        {
            lock (instances)
            {
                object instance;
                if (instances.TryGetValue(implementationType, out instance))
                {
                    return instance;
                }

                var constructor = implementationType.GetConstructors().OrderByDescending(_ => _.GetParameters().Length).FirstOrDefault();
                if (constructor == null)
                {
                    throw new InvalidOperationException(
                        String.Format(CultureInfo.InvariantCulture, "No constructor avaiable. implementation type = {0}", implementationType.Name));
                }

                var arguments = constructor.GetParameters().Select(_ => Get(_.ParameterType)).ToArray();

                instance = constructor.Invoke(arguments);

                instances.Add(implementationType, instance);

                return instance;
            }
        }
    }
}
