namespace Smart.Reflection
{
    using System;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    internal class DelegateNullableAccsessor<TTarget, TMember> : IAccessor
    {
        private readonly Func<TTarget, TMember> getter;

        private readonly Action<TTarget, TMember> setter;

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
            get { return getter != null; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool CanWrite
        {
            get { return setter != null; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        public DelegateNullableAccsessor(PropertyInfo source, Func<TTarget, TMember> getter, Action<TTarget, TMember> setter)
        {
            Source = source;
            this.getter = getter;
            this.setter = setter;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            return getter((TTarget)target);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public void SetValue(object target, object value)
        {
            setter((TTarget)target, (TMember)value);
        }
    }
}
