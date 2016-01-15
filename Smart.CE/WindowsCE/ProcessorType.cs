namespace Smart.WindowsCE
{
    /// <summary>
    /// 
    /// </summary>
    public enum ProcessorType
    {
        INTEL_386 = 386,
        INTEL_486 = 486,
        INTEL_PENTIUM = 586,
        INTEL_PENTIUMII = 686,
        MIPS_R4000 = 4000,  // incl R4101 & R3910 for Windows CE
        MIPS_R5000 = 5000,  // incl R5432 for Windows CE
        PPC_403 = 403,
        PPC_601 = 601,
        PPC_603 = 603,
        PPC_604 = 604,
        PPC_620 = 620,
        HITACHI_SH3 = 10003, // Windows CE
        HITACHI_SH3E = 10004, // Windows CE
        HITACHI_SH4 = 10005, // Windows CE
        MOTOROLA_821 = 821,   // Windows CE
        SHx_SH3 = 103,   // Windows CE
        SHx_SH3DSP = 105,   // Windows CE
        SHx_SH4 = 104,   // Windows CE
        STRONGARM = 2577,  // Windows CE - 0xA11
        ARM720 = 1824,  // Windows CE - 0x720
        ARM820 = 2080,  // Windows CE - 0x820
        ARM920 = 2336,  // Windows CE - 0x920
        ARM_7TDMI = 70001, // Windows CE
        INTEL_IA64 = 2200,
        ALPHA_21064 = 21064,
    }
}
