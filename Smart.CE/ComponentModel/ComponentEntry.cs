namespace Smart.ComponentModel
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public class ComponentEntry
    {
        /// <summary>
        ///
        /// </summary>
        public object Constant { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Type ImplementType { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="constant"></param>
        public ComponentEntry(object constant)
        {
            Constant = constant;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="implementType"></param>
        public ComponentEntry(Type implementType)
        {
            ImplementType = implementType;
        }
    }
}
