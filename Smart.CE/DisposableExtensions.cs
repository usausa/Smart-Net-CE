namespace Smart
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class DisposableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="disposable"></param>
        /// <param name="action"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static void Using<T>(this T disposable, Action<T> action) where T : IDisposable
        {
            using (disposable)
            {
                action(disposable);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="disposable"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult Using<T, TResult>(this T disposable, Func<T, TResult> func) where T : IDisposable
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            using (disposable)
            {
                return func(disposable);
            }
        }
    }
}
