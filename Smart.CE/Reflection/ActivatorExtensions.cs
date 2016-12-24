namespace Smart.Reflection
{
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class ActivatorExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static IActivator ToActivator(this ConstructorInfo ci)
        {
            return new ReflectionActivator(ci);
        }
    }
}
