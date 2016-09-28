namespace Smart.Text.Japanese
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class KanaConverter
    {
        // Roman

        private const string RomanNarrow =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        private const string RomanWide =
            "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓｔｕｖｗｘｙｚ";

        // Numeric

        private const string NumericNarrow =
            "0123456789";

        private const string NumericWide =
            "０１２３４５６７８９";

        // ASCII

        private const string AsciiNarrow =
            "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~｡｢｣､･";

        private const string AsciiWide =
            "！”＃＄％＆’（）＊＋，－．／：；＜＝＞？＠［￥］＾＿｀｛｜｝￣。「」、・";

        // Kana

        private const string KanaNarrow =
            "ｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜｦﾝﾞﾟ";

        private const string KanaWide =
            "ァィゥェォャュョッーアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワヲン゛゜";

        private const string HiraganaWide =
            "ぁぃぅぇぉゃゅょっーあいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらりるれろわをん゛゜";

        // KanaDakuon

        private const string KanaDakuonNarrow =
            "ｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾊﾋﾌﾍﾎｳﾜｦ";

        private const string KanaDakuonWide =
            "ガギグゲゴザジズゼゾダヂヅデドバビブベボヴ\u30f7\u30fa";

        private const string HiraganaDakuonWide =
            "がぎぐげござじずぜぞだぢづでどばびぶべぼヴ\u30f7\u30fa";

        // KanaHandakuon

        private const string KanaHandakuonNarrow =
            "ﾊﾋﾌﾍﾎ";

        private const string KanaHandakuonWide =
            "パピプペポ";

        private const string HiraganaHandakuonWide =
            "ぱぴぷぺぽ";

        private static readonly Dictionary<char, char> RomanNarrowToWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> RomanWideToNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> NumericNarrowToWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> NumericWideToNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> AsciiNarrowToWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> AsciiWideToNarrow = new Dictionary<char, char>();

        private static readonly Dictionary<char, char> KanaNarrowToKanaWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaWideToKanaNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaNarrowToHiraganaWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> HiraganaWideToKanaNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaWideToHiraganaWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> HiraganaWideToKanaWide = new Dictionary<char, char>();

        private static readonly Dictionary<char, char> KanaDakuonNarrowToKanaDakuonWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaDakuonWideToKanaDakuonNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaDakuonNarrowToHiraganaDakuonWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> HiraganaDakuonWideToKanaDakuonNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaDakuonWideToHiraganaDakuonWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> HiraganaDakuonWideToKanaDakuonWide = new Dictionary<char, char>();

        private static readonly Dictionary<char, char> KanaHandakuonNarrowToKanaHandakuonWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaHandakuonWideToKanaHandakuonNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaHandakuonNarrowToHiraganaHandakuonWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> HiraganaHandakuonWideToKanaHandakuonNarrow = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> KanaHandakuonWideToHiraganaHandakuonWide = new Dictionary<char, char>();
        private static readonly Dictionary<char, char> HiraganaHandakuonWideToKanaHandakuonWide = new Dictionary<char, char>();

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Ignore")]
        static KanaConverter()
        {
            for (var i = 0; i < RomanNarrow.Length; i++)
            {
                RomanNarrowToWide[RomanNarrow[i]] = RomanWide[i];
                RomanWideToNarrow[RomanWide[i]] = RomanNarrow[i];
            }

            for (var i = 0; i < NumericNarrow.Length; i++)
            {
                NumericNarrowToWide[NumericNarrow[i]] = NumericWide[i];
                NumericWideToNarrow[NumericWide[i]] = NumericNarrow[i];
            }

            for (var i = 0; i < AsciiNarrow.Length; i++)
            {
                AsciiNarrowToWide[AsciiNarrow[i]] = AsciiWide[i];
                AsciiWideToNarrow[AsciiWide[i]] = AsciiNarrow[i];
            }

            for (var i = 0; i < KanaNarrow.Length; i++)
            {
                KanaNarrowToKanaWide[KanaNarrow[i]] = KanaWide[i];
                KanaWideToKanaNarrow[KanaWide[i]] = KanaNarrow[i];

                KanaNarrowToHiraganaWide[KanaNarrow[i]] = HiraganaWide[i];
                HiraganaWideToKanaNarrow[HiraganaWide[i]] = KanaNarrow[i];

                KanaWideToHiraganaWide[KanaWide[i]] = HiraganaWide[i];
                HiraganaWideToKanaWide[HiraganaWide[i]] = KanaWide[i];
            }

            for (var i = 0; i < KanaDakuonNarrow.Length; i++)
            {
                KanaDakuonNarrowToKanaDakuonWide[KanaDakuonNarrow[i]] = KanaDakuonWide[i];
                KanaDakuonWideToKanaDakuonNarrow[KanaDakuonWide[i]] = KanaDakuonNarrow[i];

                KanaDakuonNarrowToHiraganaDakuonWide[KanaDakuonNarrow[i]] = HiraganaDakuonWide[i];
                HiraganaDakuonWideToKanaDakuonNarrow[HiraganaDakuonWide[i]] = KanaDakuonNarrow[i];

                KanaDakuonWideToHiraganaDakuonWide[KanaDakuonWide[i]] = HiraganaDakuonWide[i];
                HiraganaDakuonWideToKanaDakuonWide[HiraganaDakuonWide[i]] = KanaDakuonWide[i];
            }

            for (var i = 0; i < KanaHandakuonNarrow.Length; i++)
            {
                KanaHandakuonNarrowToKanaHandakuonWide[KanaHandakuonNarrow[i]] = KanaHandakuonWide[i];
                KanaHandakuonWideToKanaHandakuonNarrow[KanaHandakuonWide[i]] = KanaHandakuonNarrow[i];

                KanaHandakuonNarrowToHiraganaHandakuonWide[KanaHandakuonNarrow[i]] = HiraganaHandakuonWide[i];
                HiraganaHandakuonWideToKanaHandakuonNarrow[HiraganaHandakuonWide[i]] = KanaHandakuonNarrow[i];

                KanaHandakuonWideToHiraganaHandakuonWide[KanaHandakuonWide[i]] = HiraganaHandakuonWide[i];
                HiraganaHandakuonWideToKanaHandakuonWide[HiraganaHandakuonWide[i]] = KanaHandakuonWide[i];
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string Convert(string src, KanaOption option)
        {
            if (String.IsNullOrEmpty(src))
            {
                return src;
            }

            var isRomanToNarrow = (option & KanaOption.RomanToNarrow) == KanaOption.RomanToNarrow;
            var isRomanToWide = (option & KanaOption.RomanToWide) == KanaOption.RomanToWide;
            var isNumericToNarrow = (option & KanaOption.NumericToNarrow) == KanaOption.NumericToNarrow;
            var isNumericToWide = (option & KanaOption.NumericToWide) == KanaOption.NumericToWide;
            var isAsciiToNarrow = (option & KanaOption.AsciiToNarrow) == KanaOption.AsciiToNarrow;
            var isAsciiToWide = (option & KanaOption.AsciiToWide) == KanaOption.AsciiToWide;
            var isSpaceToNarrow = (option & KanaOption.SpaceToNarrow) == KanaOption.SpaceToNarrow;
            var isSpaceToWide = (option & KanaOption.SpaceToWide) == KanaOption.SpaceToWide;
            var isKanaToNarrow = (option & KanaOption.KanaToNarrow) == KanaOption.KanaToNarrow;
            var isKanaToWide = (option & KanaOption.KanaToWide) == KanaOption.KanaToWide;
            var isHiraganaToHankana = (option & KanaOption.HiraganaToHankana) == KanaOption.HiraganaToHankana;
            var isHankanaToHiragana = (option & KanaOption.HankanaToHiragana) == KanaOption.HankanaToHiragana;
            var isKatakanaToHiragana = (option & KanaOption.KatakanaToHiragana) == KanaOption.KatakanaToHiragana;
            var isHiraganaToKatakana = (option & KanaOption.HiraganaToKatakana) == KanaOption.HiraganaToKatakana;

            var sb = new StringBuilder();
            for (var i = 0; i < src.Length; i++)
            {
                var c = src[i];
                var next = i < src.Length - 1 ? src[i + 1] : (char)0;
                char convert;

                // Roman
                if (isRomanToNarrow && RomanWideToNarrow.TryGetValue(c, out convert))
                {
                    sb.Append(convert);
                    continue;
                }

                if (isRomanToWide && RomanNarrowToWide.TryGetValue(c, out convert))
                {
                    sb.Append(convert);
                    continue;
                }

                // Numeric
                if (isNumericToNarrow && NumericWideToNarrow.TryGetValue(c, out convert))
                {
                    sb.Append(convert);
                    continue;
                }

                if (isNumericToWide && NumericNarrowToWide.TryGetValue(c, out convert))
                {
                    sb.Append(convert);
                    continue;
                }

                // ASCII
                if (isAsciiToNarrow && AsciiWideToNarrow.TryGetValue(c, out convert))
                {
                    sb.Append(convert);
                    continue;
                }

                if (isAsciiToWide && AsciiNarrowToWide.TryGetValue(c, out convert))
                {
                    sb.Append(convert);
                    continue;
                }

                // Space
                if (isSpaceToNarrow && (c == '　'))
                {
                    sb.Append(' ');
                    continue;
                }

                if (isSpaceToWide && (c == ' '))
                {
                    sb.Append('　');
                    continue;
                }

                // WideKatakana/NarrowKatakana
                if (isKanaToNarrow)
                {
                    if (KanaDakuonWideToKanaDakuonNarrow.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        sb.Append('ﾞ');
                        continue;
                    }

                    if (KanaHandakuonWideToKanaHandakuonNarrow.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        sb.Append('ﾟ');
                        continue;
                    }

                    if (KanaWideToKanaNarrow.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }
                }

                if (isKanaToWide)
                {
                    if ((next == 'ﾞ') && KanaDakuonNarrowToKanaDakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        i++;
                        continue;
                    }

                    if ((next == 'ﾟ') && KanaHandakuonNarrowToKanaHandakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        i++;
                        continue;
                    }

                    if (KanaNarrowToKanaWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }
                }

                // WideHiragana/NarrowKatanaka
                if (isHiraganaToHankana)
                {
                    if (HiraganaDakuonWideToKanaDakuonNarrow.TryGetValue(c, out convert))
                    {
                        sb.Append(convert).Append('ﾞ');
                        continue;
                    }

                    if (HiraganaHandakuonWideToKanaHandakuonNarrow.TryGetValue(c, out convert))
                    {
                        sb.Append(convert).Append('ﾟ');
                        continue;
                    }

                    if (HiraganaWideToKanaNarrow.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }
                }

                if (isHankanaToHiragana)
                {
                    if ((next == 'ﾞ') && KanaDakuonNarrowToHiraganaDakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        i++;
                        continue;
                    }

                    if ((next == 'ﾟ') && KanaHandakuonNarrowToHiraganaHandakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        i++;
                        continue;
                    }

                    if (KanaNarrowToHiraganaWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }
                }

                // WideHiragana/WideKatakana
                if (isKatakanaToHiragana)
                {
                    if (KanaDakuonWideToHiraganaDakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }

                    if (KanaHandakuonWideToHiraganaHandakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }

                    if (KanaWideToHiraganaWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }
                }

                if (isHiraganaToKatakana)
                {
                    if (HiraganaDakuonWideToKanaDakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }

                    if (HiraganaHandakuonWideToKanaHandakuonWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }

                    if (HiraganaWideToKanaWide.TryGetValue(c, out convert))
                    {
                        sb.Append(convert);
                        continue;
                    }
                }

                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
