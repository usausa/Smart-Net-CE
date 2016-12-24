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
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", Justification = "Ignore")]
        T Get<T>();

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T TryGet<T>();

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
        object TryGet(Type componentType);

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        object TryGet(Type componentType, out bool result);

        /// <summary>
        ///
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        IEnumerable<object> GetAll(Type componentType);
    }
}
