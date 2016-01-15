﻿namespace Smart.Win32
{
    /// <summary>
    /// 
    /// </summary>
    public enum WM
    {
        NULL                 = 0,
        CREATE               = 1,
        DESTROY              = 2,
        MOVE                 = 3,
        SIZE                 = 5,
        ACTIVATE             = 6,
        SETFOCUS             = 7,
        KILLFOCUS            = 8,
        ENABLE               = 10,
        SETTEXT              = 12,
        GETTEXT              = 13,
        GETTEXTLENGTH        = 14,
        PAINT                = 15,
        CLOSE                = 16,
        QUIT                 = 18,
        ENDSESSION           = 22,
        SHOWWINDOW           = 24,
        CTLCOLOR             = 25,
        WININICHANGE         = 26,
        ACTIVATEAPP          = 28,
        FONTCHANGE           = 29,
        TIMECHANGE           = 30,
        CANCELMODE           = 31,
        SETCURSOR            = 32,
        MOUSEACTIVATE        = 33,
        CHILDACTIVATE        = 34,
        QUEUESYNC            = 35,
        GETMINMAXINFO        = 36,
        PAINTICON            = 38,
        ICONERASEBKGND       = 39,
        NEXTDLGCTL           = 40,
        DRAWITEM             = 43,
        MEASUREITEM          = 44,
        DELETEITEM           = 45,
        VKEYTOITEM           = 46,
        CHARTOITEM           = 47,
        SETFONT              = 48,
        GETFONT              = 49,
        SETHOTKEY            = 50,
        GETHOTKEY            = 51,
        QUERYDRAGICON        = 55,
        COMPAREITEM          = 57,
        GETOBJECT            = 61,
        COMPACTING           = 65,
        COMMNOTIFY           = 68,
        WINDOWPOSCHANGING    = 70,
        WINDOWPOSCHANGED     = 71,
        POWER                = 72,
        COPYDATA             = 74,
        CANCELJOURNAL        = 75,
        NOTIFY               = 78,
        INPUTLANGCHANGEREQUEST = 80,
        INPUTLANGCHANGE      = 81,
        TCARD                = 82,
        HELP                 = 83,
        USERCHANGED          = 84,
        NOTIFYFORMAT         = 85,

        CONTEXTMENU          = 123,
        STYLECHANGING        = 124,
        STYLECHANGED         = 125,
        DISPLAYCHANGE        = 126,
        GETICON              = 127,
        SETICON              = 128,
        NCCREATE             = 129,
        NCDESTROY            = 130,
        NCCALCSIZE           = 131,
        NCHITTEST            = 132,
        NCPAINT              = 133,
        NCACTIVATE           = 134,
        GETDLGCODE           = 135,

        KEYDOWN              = 256,
        KEYUP                = 257,
        CHAR                 = 258,
        DEADCHAR             = 259,
        SYSKEYDOWN           = 260,
        SYSKEYUP             = 261,
        SYSCHAR              = 262,
        SYSDEADCHAR          = 263,
        KEYLAST              = 264,

        IME_STARTCOMPOSITION = 0x010D,
        IME_ENDCOMPOSITION   = 0x010E,
        IME_COMPOSITION      = 0x010F,
        IME_KEYLAST          = 0x010F,
        IME_SETCONTEXT       = 0x0281,
        IME_NOTIFY           = 0x0282,
        IME_CONTROL          = 0x0283,
        IME_COMPOSITIONFULL  = 0x0284,
        IME_SELECT           = 0x0285,
        IME_CHAR             = 0x0286,
        IME_SYSTEM           = 0x0287,
        IME_REQUEST          = 0x0288,
        IME_KEYDOWN          = 0x0290,
        IME_KEYUP            = 0x0291,

        INITDIALOG           = 272,
        COMMAND              = 273,
        SYSCOMMAND           = 274,
        TIMER                = 275,
        HSCROLL              = 276,
        VSCROLL              = 277,
        INITMENU             = 278,
        INITMENUPOPUP        = 279,
        MENUSELECT           = 287,
        MENUCHAR             = 288,
        ENTERIDLE            = 289,

        CTLCOLORMSGBOX       = 306,
        CTLCOLOREDIT         = 307,
        CTLCOLORLISTBOX      = 308,
        CTLCOLORBTN          = 309,
        CTLCOLORDLG          = 310,
        CTLCOLORSCROLLBAR    = 311,
        CTLCOLORSTATIC       = 312,

        MOUSEMOVE            = 512,
        LBUTTONDOWN          = 513,
        LBUTTONUP            = 514,
        LBUTTONDBLCLK        = 515,
        RBUTTONDOWN          = 516,
        RBUTTONUP            = 517,
        RBUTTONDBLCLK        = 518,
        MBUTTONDOWN          = 519,
        MBUTTONUP            = 520,
        MBUTTONDBLCLK        = 521,
        MOUSEWHEEL           = 522,
        PARENTNOTIFY         = 528,
        ENTERMENULOOP        = 529,
        EXITMENULOOP         = 530,
        NEXTMENU             = 531,
        SIZING               = 532,
        CAPTURECHANGED       = 533,
        MOVING               = 534,
        POWERBROADCAST       = 536,
        DEVICECHANGE         = 537,
        MDICREATE            = 544,
        MDIDESTROY           = 545,
        MDIACTIVATE          = 546,
        MDIRESTORE           = 547,
        MDINEXT              = 548,
        MDIMAXIMIZE          = 549,
        MDITILE              = 550,
        MDICASCADE           = 551,
        MDIICONARRANGE       = 552,
        MDIGETACTIVE         = 553,
        MDISETMENU           = 560,
        ENTERSIZEMOVE        = 561,
        EXITSIZEMOVE         = 562,
        DROPFILES            = 563,
        MDIREFRESHMENU       = 564, 

        MOUSEHOVER           = 673,
        MOUSELEAVE           = 675,

        CUT                  = 768,
        COPY                 = 769,
        PASTE                = 770,
        CLEAR                = 771,
        UNDO                 = 772,
        RENDERFORMAT         = 773,
        RENDERALLFORMATS     = 774,
        DESTROYCLIPBOARD     = 775,
        DRAWCLIPBOARD        = 776,
        PAINTCLIPBOARD       = 777,
        VSCROLLCLIPBOARD     = 778,
        SIZECLIPBOARD        = 779,
        ASKCBFORMATNAME      = 780,
        CHANGECBCHAIN        = 781,
        HSCROLLCLIPBOARD     = 782,
        QUERYNEWPALETTE      = 783,
        PALETTEISCHANGING    = 784,
        PALETTECHANGED       = 785,
        HOTKEY               = 786,
        PRINT                = 791,
        PRINTCLIENT          = 792,

        USER                 = 1024,

        DBNOTIFICATION       = 0x03FD,
        NETCONNECT           = 0x03FE,
        HIBERNATE            = 0x03FF,

        APP                  = 0x8000,
    }
}
