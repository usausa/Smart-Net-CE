namespace Smart.ComponentModel
{
    /// <summary>
    ///
    /// </summary>
    public static class ComponentConfigExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ComponentContainer ToContainer(this IComponentConfig config)
        {
            return new ComponentContainer(config);
        }
    }
}
