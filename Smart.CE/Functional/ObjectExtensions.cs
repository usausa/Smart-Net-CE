namespace Smart.Functional
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="action"></param>
        public static void Apply<T>(this T value, Action<T> action)
        {
            action(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Apply<T, TResult>(this T value, Func<T, TResult> func)
        {
            return func(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Or<T, TResult>(this T value, Func<T, TResult> func) where T : class
        {
            return value == null ? default(TResult) : func(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Or<T, TResult>(this T? value, Func<T, TResult> func) where T : struct
        {
            return value == null ? default(TResult) : func(value.Value);
        }
    }
}
