namespace Smart.Windows.Forms
{
    using System;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public static class SendKeys
    {
        private const int KEYEVENTF_KEYDOWN = 0;
        private const int KEYEVENTF_KEYUP = 2;
        //private const int KEYEVENTF_SILENT = 4;

        private const int SHIFTVK = 0x10;
        private const int CONTROLVK = 0x11;
        private const int ALTVK = 0x12;

        private const int SHIFTPOS = 0;
        private const int CONTROLPOS = 1;
        private const int ALTPOS = 2;

        /// <summary>
        ///
        /// </summary>
        private class KeywordVk
        {
            public string Keyword { get; private set; }
            public int Vk { get; private set; }
            public bool Shift { get; private set; }

            public KeywordVk(string keyword, int vk)
                : this(keyword, vk, false)
            {
            }

            public KeywordVk(string keyword, int vk, bool shift)
            {
                Keyword = keyword;
                Vk = vk;
                Shift = shift;
            }
        }

        private static readonly KeywordVk[] Keywords =
        {
            new KeywordVk("ENTER", 13),
            new KeywordVk("TAB", 0x9),
            new KeywordVk("ESC", 0x1b), new KeywordVk("ESCAPE", 0x1b),

            new KeywordVk("HOME", 0x24),
            new KeywordVk("END", 0x23),
            new KeywordVk("LEFT", 0x25),
            new KeywordVk("RIGHT", 0x27),
            new KeywordVk("UP", 0x26),
            new KeywordVk("DOWN", 0x28),
            new KeywordVk("PGUP", 0x21),
            new KeywordVk("PGDN", 0x22),
            new KeywordVk("NUMLOCK", 0x90),
            new KeywordVk("SCROLLLOCK", 0x91),
            new KeywordVk("PRTSC", 0x2c),
            new KeywordVk("BREAK", 0x3),

            new KeywordVk("BACKSPACE", 0x8), new KeywordVk("BKSP", 0x8), new KeywordVk("BS", 0x8),
            new KeywordVk("CLEAR", 0xc),
            new KeywordVk("CAPSLOCK", 0x14),
            new KeywordVk("INS", 0x2d), new KeywordVk("INSERT", 0x2d),
            new KeywordVk("DEL", 0x2e), new KeywordVk("DELETE", 0x2e),
            new KeywordVk("HELP", 0x2f),

            new KeywordVk("F1", 0x70),
            new KeywordVk("F2", 0x71),
            new KeywordVk("F3", 0x72),
            new KeywordVk("F4", 0x73),
            new KeywordVk("F5", 0x74),
            new KeywordVk("F6", 0x75),
            new KeywordVk("F7", 0x76),
            new KeywordVk("F8", 0x77),
            new KeywordVk("F9", 0x78),
            new KeywordVk("F10", 0x79),
            new KeywordVk("F11", 0x7a),
            new KeywordVk("F12", 0x7b),
            new KeywordVk("F13", 0x7c),
            new KeywordVk("F14", 0x7d),
            new KeywordVk("F15", 0x7e),
            new KeywordVk("F16", 0x7f),

            new KeywordVk("MULTIPLY", 0x6a),
            new KeywordVk("ADD", 0x6b),
            new KeywordVk("SUBTRACT", 0x6d),
            new KeywordVk("DIVIDE", 0x6f),
            new KeywordVk("+", 0x6b),

            new KeywordVk("%", 0x35, true),
            new KeywordVk("(", 0x39, true),
            new KeywordVk(")", 0x30, true),
            new KeywordVk("~", 0xc0, true),
            new KeywordVk("[", 0xdb),
            new KeywordVk("{", 0xdb, true),
            new KeywordVk("]", 0xdd),
            new KeywordVk("}", 0xdd, true)
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private static KeywordVk MatchKeyword(string keyword)
        {
            return Keywords.FirstOrDefault(t => String.Compare(t.Keyword, keyword, StringComparison.Ordinal) == 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys"></param>
        public static void Send(string keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException("keys");
            }

            var mods = new byte[3];

            var index = 0;
            while (index < keys.Length)
            {
                var ch = keys[index];

                switch (ch)
                {
                    case '+':
                        if (mods[SHIFTPOS] != 0)
                        {
                            throw new ArgumentException("+");
                        }
                        mods[SHIFTPOS] = 4;
                        SendKey(SHIFTVK, KEYEVENTF_KEYDOWN);
                        break;

                    case '^':
                        if (mods[CONTROLPOS] != 0)
                        {
                            throw new ArgumentException("^");
                        }
                        mods[CONTROLPOS] = 4;
                        SendKey(CONTROLVK, KEYEVENTF_KEYDOWN);
                        break;

                    case '%':
                        if (mods[ALTPOS] != 0)
                        {
                            throw new ArgumentException("%");
                        }
                        mods[ALTPOS] = 4;
                        SendKey(ALTVK, KEYEVENTF_KEYDOWN);
                        break;

                    case '~': // ENTER key
                        SendChar(13, mods);
                        break;

                    case '{':
                        index++;
                        var pos = keys.IndexOf('}', index);
                        if (pos < 0)
                        {
                            throw new ArgumentException("}");
                        }
                        var str = keys.Substring(index, pos - index);
                        // {}}?
                        if (str.Length == 0)
                        {
                            index++;
                            if ((index >= keys.Length) || keys[index] != '}')
                            {
                                throw new ArgumentException("{}");
                            }
                            mods[SHIFTPOS] = 4;
                            SendKey(SHIFTVK, 0);
                            SendChar(0xdd, mods);
                        }
                        else
                        {
                            index += str.Length;
                            var strs = str.Split(new[] { ' ' });
                            if (strs.Length > 2)
                            {
                                throw new ArgumentException("{...}");
                            }

                            var count = 1;
                            if (strs.Length == 2)
                            {
                                str = strs[0];
                                count = Convert.ToInt32(strs[1], CultureInfo.InvariantCulture);
                            }

                            var vk = MatchKeyword(str);
                            if (vk == null)
                            {
                                throw new ArgumentException(str);
                            }

                            for (var i = 0; i < count; i++)
                            {
                                if (vk.Shift)
                                {
                                    mods[SHIFTPOS] = 4;
                                    SendKey(SHIFTVK, 0);
                                }
                                SendChar((byte)vk.Vk, mods);
                            }
                        }
                        break;

                    default:
                        // [MEMO] 現状、SHIFT + * のキーサポートが不十分
                        if ((ch >= 'A') && (ch <= 'Z'))
                        {
                            mods[SHIFTPOS] = 4;
                            SendKey(SHIFTVK, 0);
                        }
                        if ((ch >= 'a') && (ch <= 'z'))
                        {
                            ch = (char)(ch - ' ');
                        }
                        SendChar((byte)ch, mods);
                        break;
                }

                index++;
            }

            CancelMods(mods, 4);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="k"></param>
        /// <param name="flags"></param>
        private static void SendKey(byte k, int flags)
        {
            NativeMethods.keybd_event(k, 0, flags, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="k"></param>
        /// <param name="mods"></param>
        private static void SendChar(byte k, byte[] mods)
        {
            NativeMethods.keybd_event(k, 0, 0, 0);
            NativeMethods.keybd_event(k, 0, KEYEVENTF_KEYUP, 0);

            CancelMods(mods, 4);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="level"></param>
        private static void CancelMods(byte[] keys, int level)
        {
            SetMods(keys, level, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="level"></param>
        /// <param name="value"></param>
        private static void SetMods(byte[] keys, int level, byte value)
        {
            if (keys[SHIFTPOS] == level)
            {
                if (value == 0)
                {
                    SendKey(SHIFTVK, KEYEVENTF_KEYUP);
                }
                keys[SHIFTPOS] = value;
            }
            if (keys[CONTROLPOS] == level)
            {
                if (value == 0)
                {
                    SendKey(CONTROLVK, KEYEVENTF_KEYUP);
                }
                keys[CONTROLPOS] = value;
            }
            if (keys[ALTPOS] == level)
            {
                if (value == 0)
                {
                    SendKey(ALTVK, KEYEVENTF_KEYUP);
                }
                keys[ALTPOS] = value;
            }
        }
    }
}
