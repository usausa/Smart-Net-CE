namespace Smart.ComponentModel.DataAnnotations
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DisplayAttribute : Attribute
    {
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }
    }
}
