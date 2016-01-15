namespace Smart
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class DesignMode
    {
        private static readonly bool IsDesignMode = AppDomain.CurrentDomain.FriendlyName.Contains("DefaultDomain");

        /// <summary>
        /// 
        /// </summary>
        public static bool IsTrue
        {
            get { return IsDesignMode; }
        }
    }
}
