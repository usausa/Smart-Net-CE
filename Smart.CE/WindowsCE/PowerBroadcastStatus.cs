namespace Smart.WindowsCE
{
    /// <summary>
    /// 
    /// </summary>
    public enum PowerBroadcastStatus
    {
        QuerySuspend = 0x0000,
        QueryStandby = 0x0001,
        QuerySuspendFailed = 0x0002,
        QueryStandbyFailed = 0x0003,
        Suspend = 0x0004,
        Standby = 0x0005,
        ResumeCritical = 0x0006,
        ResumeSuspend = 0x0007,
        ResumeStandby = 0x0008,
        BatteryLow = 0x0009,
        PowerStatusChange = 0x000a,
        OemEvent = 0x000b,
        ResumeAutomatic = 0x0012,
    }
}
