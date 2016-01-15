namespace Smart.Win32
{
    /// <summary>
    /// Edit Message
    /// </summary>
    public enum EM
    {
        GETSEL              = 0x00b0,
        SETSEL              = 0x00b1,
        GETRECT             = 0x00b2,
        SETRECT             = 0x00b3,
        SETRECTNP           = 0x00b4,
        SCROLL              = 0x00b5,
        LINESCROLL          = 0x00b6,
        SCROLLCARET         = 0x00b7,
        GETMODIFY           = 0x00b8,
        SETMODIFY           = 0x00b9,
        GETLINECOUNT        = 0x00ba,
        LINEINDEX           = 0x00bb,
        LINELENGTH          = 0x00c1,
        REPLACESEL          = 0x00c2,
        GETLINE             = 0x00c4,
        LIMITTEXT           = 0x00c5,
        CANUNDO             = 0x00c6,
        UNDO                = 0x00c7,
        FMTLINES            = 0x00c8,
        LINEFROMCHAR        = 0x00c9,
        SETTABSTOPS         = 0x00cb,
        SETPASSWORDCHAR     = 0x00cc,
        EMPTYUNDOBUFFER     = 0x00cd,
        GETFIRSTVISIBLELINE = 0x00ce,
        SETREADONLY         = 0x00cf,
        GETPASSWORDCHAR     = 0x00d2,
        SETMARGINS          = 0x00d3,
        GETMARGINS          = 0x00d4,
        SETLIMITTEXT        = LIMITTEXT,
        GETLIMITTEXT        = 0x00d5,
        POSFROMCHAR         = 0x00d6,
        CHARFROMPOS         = 0x00d7,
    }
}
