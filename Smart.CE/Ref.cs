namespace Smart
{
    using System;

    /// <summary>
    /// ex)
    /// int x;
    /// var refX = new Ref(() => { return x; }, (value) => { x = value; })
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Ref<T>
    {
        private readonly Func<T> getter;

        private readonly Action<T> setter;

        /// <summary>
        ///
        /// </summary>
        public T Value
        {
            get { return getter(); }
            set { setter(value); }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="getter"></param>
        /// <param name="setter"></param>
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}
