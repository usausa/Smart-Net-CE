namespace Smart.Drawing
{
    /// <summary>
    /// 
    /// </summary>
    public enum Border3DStyle
    {
        Flat = 0x400A,

        /// <summary>
        /// ボーダー2の浮き出し用
        /// </summary>
        Raised = 0x0005,

        /// <summary>
        /// ボーダー1の浮き出し用
        /// </summary>
        RaisedInner = 0x0004,
        
        /// <summary>
        /// ボーダー2の窪み用
        /// </summary>
        Sunken = 0x000A,

        /// <summary>
        /// ボーダー1の窪み用
        /// </summary>
        SunkenOuter = 0x0002
    }
}
