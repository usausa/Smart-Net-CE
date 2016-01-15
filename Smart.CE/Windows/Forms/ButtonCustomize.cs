namespace Smart.Windows.Forms
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum ButtonCustomize
    {
        Pressed = 0x01,
        Focused = 0x02,
        Disabled = 0x04,

        PressedFocused = Pressed | Focused,
        PressedDisabled = Pressed | Disabled,
        FocusedDisabled = Focused | Disabled,

        All = Pressed | Focused | Disabled,
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ButtonCustomizeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customize"></param>
        /// <returns></returns>
        public static bool HasPressed(this ButtonCustomize customize)
        {
            return (customize & ButtonCustomize.Pressed) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customize"></param>
        /// <returns></returns>
        public static bool HasFocused(this ButtonCustomize customize)
        {
            return (customize & ButtonCustomize.Focused) != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customize"></param>
        /// <returns></returns>
        public static bool HasDisabled(this ButtonCustomize customize)
        {
            return (customize & ButtonCustomize.Disabled) != 0;
        }
    }
}
