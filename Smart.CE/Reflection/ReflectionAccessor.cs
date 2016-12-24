namespace Smart.Reflection
{
    using System;
    using System.Reflection;

    internal class ReflectionAccessor : IAccessor
    {
        /// <summary>
        ///
        /// </summary>
        public PropertyInfo Source { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public string Name
        {
            get { return Source.Name; }
        }

        /// <summary>
        ///
        /// </summary>
        public Type Type
        {
            get { return Source.PropertyType; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool CanRead
        {
            get { return Source.CanRead; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool CanWrite
        {
            get { return Source.CanWrite; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        public ReflectionAccessor(PropertyInfo source)
        {
            Source = source;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            return Source.GetValue(target, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            Source.SetValue(target, value, null);
        }
    }
}
