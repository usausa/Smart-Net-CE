namespace Smart.Functional
{
    using System;

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
            get { return this != None; }
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
    }

    /// <summary>
    ///
    /// </summary>
    public static class MaybeExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> ToMaybe<T>(this T value) where T : class
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> ToMaybe<T>(this T? value) where T : struct
        {
            return value == null ? Maybe<T>.None : new Maybe<T>(value.Value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Maybe<TResult> Bind<T, TResult>(this Maybe<T> maybe, Func<T, Maybe<TResult>> g)
        {
            return maybe.HasValue ? g(maybe.Value) : Maybe<TResult>.None;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetOr<T>(this Maybe<T> maybe, T defaultValue)
        {
            return maybe.HasValue ? maybe.Value : defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public static T GetOr<T>(this Maybe<T> maybe, Func<T> defaultValue)
        {
            return maybe.HasValue ? maybe.Value : defaultValue();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="action"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public static void IfPresent<T>(this Maybe<T> maybe, Action<T> action)
        {
            if (maybe.HasValue)
            {
                action(maybe.Value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public static TResult Match<T, TResult>(this Maybe<T> maybe, Func<T, TResult> some, Func<TResult> none)
        {
            return maybe.HasValue ? some(maybe.Value) : none();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maybe"></param>
        /// <param name="some"></param>
        /// <param name="none"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Performance")]
        public static void Match<T>(this Maybe<T> maybe, Action<T> some, Action none)
        {
            if (maybe.HasValue)
            {
                some(maybe.Value);
            }
            else
            {
                none();
            }
        }
    }
}
