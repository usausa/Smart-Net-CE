﻿namespace Smart.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        ///
        /// </summary>
        PropertyInfo Source { get; }

        /// <summary>
        ///
        /// </summary>
        string Name { get; }

        /// <summary>
        ///
        /// </summary>
        Type Type { get; }

        /// <summary>
        ///
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        ///
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        object GetValue(object target);

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        void SetValue(object target, object value);
    }
}
