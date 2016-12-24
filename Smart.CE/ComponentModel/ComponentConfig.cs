namespace Smart.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public class ComponentConfig : IComponentConfig
    {
        private readonly IDictionary<Type, List<ComponentEntry>> mappings = new Dictionary<Type, List<ComponentEntry>>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        private List<ComponentEntry> GetEntries(Type componentType)
        {
            List<ComponentEntry> entries;
            if (mappings.TryGetValue(componentType, out entries))
            {
                return entries;
            }

            entries = new List<ComponentEntry>();
            mappings[componentType] = entries;

            return entries;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public void Add<TComponent, TImplement>()
            where TImplement : TComponent
        {
            Add(typeof(TComponent), typeof(TImplement));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        public void Add<TComponent>()
        {
            var type = typeof(TComponent);
            Add(type, type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        public void Add(Type componentType)
        {
            Add(componentType, componentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="implementType"></param>
        public void Add(Type componentType, Type implementType)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException("componentType");
            }

            if (implementType == null)
            {
                throw new ArgumentNullException("implementType");
            }

            var entries = GetEntries(componentType);
            entries.Add(new ComponentEntry(implementType));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="constant"></param>
        public void Add<TComponent>(TComponent constant)
        {
            Add(typeof(TComponent), constant);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="constant"></param>
        public void Add(Type componentType, object constant)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException("componentType");
            }

            if (constant == null)
            {
                throw new ArgumentNullException("constant");
            }

            var entries = GetEntries(componentType);
            entries.Add(new ComponentEntry(constant));
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
            mappings.Remove(componentType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public void Remove<TComponent, TImplement>()
        {
            Remove(typeof(TComponent), typeof(TImplement));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="implementType"></param>
        public void Remove(Type componentType, Type implementType)
        {
            List<ComponentEntry> list;
            if (!mappings.TryGetValue(componentType, out list))
            {
                return;
            }

            list.RemoveAll(x => x.ImplementType != null && x.ImplementType == implementType);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="constant"></param>
        public void Remove<TComponent>(TComponent constant)
        {
            Remove(typeof(TComponent), constant);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="constant"></param>
        public void Remove(Type componentType, object constant)
        {
            List<ComponentEntry> list;
            if (!mappings.TryGetValue(componentType, out list))
            {
                return;
            }

            list.RemoveAll(x => x.Constant != null && x.Constant == constant);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IDictionary<Type, ComponentEntry[]> ToMappings()
        {
            return mappings.ToDictionary(x => x.Key, x => x.Value.ToArray());
        }
    }
}
