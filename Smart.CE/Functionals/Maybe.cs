namespace Smart.Functionals
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> Make<T>(T value) where T : class
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> Make<T>(T? value) where T : struct 
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value.Value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Maybe<T>
    {
        internal static readonly Maybe<T> None = new Maybe<T>();
        
        private readonly T value;

        /// <summary>
        /// 
        /// </summary>
        public T Value
        {
            get { return value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasValue
        {
            get
            {
                return this != None;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal Maybe()
        {
            value = default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        internal Maybe(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetOr(T defaultValue)
        {
            return HasValue ? Value : defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public T GetOr(Func<T> defaultValue)
        {
            return HasValue ? Value : defaultValue();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public void IfPresent(Action<T> action)
        {
            if (HasValue)
            {
                action(Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
        {
            return HasValue ? some(value) : none();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="some"></param>
        /// <param name="none"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public void Match(Action<T> some, Action none)
        {
            if (HasValue)
            {
                some(value);
            }
            else
            {
                none();
            }
        }
    }
}
