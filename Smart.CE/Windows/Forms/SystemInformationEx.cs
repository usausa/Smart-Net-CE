namespace Smart.Windows.Forms
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    public static class SystemInformationEx
    {
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private const int SM_CXVSCROLL = 2;
        private const int SM_CYHSCROLL = 3;
        private const int SM_CYCAPTION = 4;
        private const int SM_CXDLGFRAME = 7;
        private const int SM_CYDLGFRAME = 8;
        private const int SM_CXICON = 11;
        private const int SM_CYICON = 12;
        private const int SM_CXCURSOR = 13;
        private const int SM_CYCURSOR = 14;
        private const int SM_MOUSEPRESENT = 19;
        private const int SM_CYVSCROLL = 20;
        private const int SM_CXHSCROLL = 21;
        private const int SM_DEBUG = 22;
        private const int SM_CXDOUBLECLK = 36;
        private const int SM_CYDOUBLECLK = 37;
        private const int SM_CXICONSPACING = 38;
        private const int SM_CYICONSPACING = 39;
        private const int SM_CXSMICON = 49;
        private const int SM_CYSMICON = 50;

        /// <summary>
        /// 
        /// </summary>
        public static Size ScreenSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXSCREEN), NativeMethods.GetSystemMetrics(SM_CYSCREEN));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int VerticalScrollBarArrowHeight
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_CYVSCROLL);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int VerticalScrollBarWidth
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_CXVSCROLL);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int HorizontalScrollBarArrowWidth
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_CXHSCROLL);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int HorizontalScrollBarHeight
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_CYHSCROLL);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int CaptionHeight
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_CYCAPTION);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size FixedFrameBorderSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXDLGFRAME), NativeMethods.GetSystemMetrics(SM_CYDLGFRAME));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size IconSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXICON), NativeMethods.GetSystemMetrics(SM_CYICON));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size CursorSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXCURSOR), NativeMethods.GetSystemMetrics(SM_CYCURSOR));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool DebugOS
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_DEBUG) != 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool MousePresent
        {
            get
            {
                return NativeMethods.GetSystemMetrics(SM_MOUSEPRESENT) != 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size DoubleClickSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXDOUBLECLK), NativeMethods.GetSystemMetrics(SM_CYDOUBLECLK));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size IconSpacingSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXICONSPACING), NativeMethods.GetSystemMetrics(SM_CYICONSPACING));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size SmallIconSize
        {
            get
            {
                return new Size(NativeMethods.GetSystemMetrics(SM_CXSMICON), NativeMethods.GetSystemMetrics(SM_CYSMICON));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Size BorderSize
        {
            get
            {
                return SystemInformation.BorderSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int DoubleClickTime
        {
            get
            {
                return SystemInformation.DoubleClickTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int MenuHeight
        {
            get
            {
                return SystemInformation.MenuHeight;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Rectangle WorkingArea
        {
            get
            {
                return Screen.PrimaryScreen.WorkingArea;
            }
        }
    }
}