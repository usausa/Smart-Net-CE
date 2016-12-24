namespace Smart.Reflection
{
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public interface IActivator
    {
        /// <summary>
        ///
        /// </summary>
        ConstructorInfo Source { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        object Create(params object[] arguments);
    }
}
