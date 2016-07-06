namespace Smart.Text.Japanese
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [Flags]
    public enum KanaOption
    {
        RomanToNarrow = 0x00000001,         // r
        RomanToWide = 0x00000002,           // R
        NumericToNarrow = 0x00000010,       // n
        NumericToWide = 0x00000020,         // N
        AsciiToNarrow = 0x00000100,         // a
        AsciiToWide = 0x00000200,           // A
        SpaceToNarrow = 0x00001000,         // s
        SpaceToWide = 0x00002000,           // S
        KanaToNarrow = 0x00010000,          // k
        KanaToWide = 0x00020000,            // K
        HiraganaToHankana = 0x00100000,     // h
        HankanaToHiragana = 0x00200000,     // H
        KatakanaToHiragana = 0x01000000,    // c
        HiraganaToKatakana = 0x02000000,    // C

        Wide = RomanToWide | NumericToWide | AsciiToNarrow | KanaToWide,
        Narrow = RomanToNarrow | NumericToNarrow | AsciiToNarrow | KanaToNarrow,
    }
}
