namespace Smart.Reflection
{
    using System;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TTarget"></typeparam>
    /// <typeparam name="TMember"></typeparam>
    internal class NonNullableDelegateAccsessor<TTarget, TMember> : IAccessor
    {
        private readonly Func<TTarget, TMember> getter;

        private readonly Action<TTarget, TMember> setter;

        private readonly TMember nullValue;

        /// <summary>
        ///
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public Type Type { get; private set; }

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
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        /// <param name="nullValue"></param>
        public NonNullableDelegateAccsessor(string name, Type type, Func<TTarget, TMember> getter, Action<TTarget, TMember> setter, TMember nullValue)
        {
            Name = name;
            Type = type;
            this.getter = getter;
            this.setter = setter;
            this.nullValue = nullValue;
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
            setter((TTarget)target, value == null ? nullValue : (TMember)value);
        }
    }
}
