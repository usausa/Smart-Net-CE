namespace Smart.ComponentModel
{
    using System.ComponentModel;

    /// <summary>
    /// 
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:パブリック メソッドの引数の検証", Justification = "Extensions")]
        public static bool IsDesignMode(this IComponent target)
        {
            var site = target.Site;
            return site != null && site.DesignMode;
        }
    }
}
