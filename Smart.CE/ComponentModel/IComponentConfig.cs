namespace Smart.ComponentModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public interface IComponentConfig
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IDictionary<Type, ComponentEntry[]> ToMappings();
    }
}
