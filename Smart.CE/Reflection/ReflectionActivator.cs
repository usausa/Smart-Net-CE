namespace Smart.Reflection
{
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public class ReflectionActivator : IActivator
    {
        /// <summary>
        ///
        /// </summary>
        public ConstructorInfo Source { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="constructor"></param>
        public ReflectionActivator(ConstructorInfo constructor)
        {
            Source = constructor;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public object Create(params object[] arguments)
        {
            return Source.Invoke(arguments);
        }
    }
}
