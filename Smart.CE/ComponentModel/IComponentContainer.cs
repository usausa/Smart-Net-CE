namespace Smart.ComponentModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public interface IComponentContainer : IDisposable, IServiceProvider
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        void Add<TComponent, TImplement>()
            where TImplement : TComponent;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        void RemoveAll<TComponent>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        void RemoveAll(Type componentType);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Ignore")]
        T Get<T>();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Ignore")]
        object Get(Type componentType);

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        IEnumerable<object> GetAll(Type componentType);
    }
}
