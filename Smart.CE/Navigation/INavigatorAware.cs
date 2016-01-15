namespace Smart.Navigation
{
    /// <summary>
    /// ナビゲーター対応
    /// </summary>
    public interface INavigatorAware
    {
        /// <summary>
        /// ナビゲーター設定
        /// </summary>
        /// <param name="navigator">ナビゲーター</param>
        void SetNavigator(Navigator navigator);
    }
}