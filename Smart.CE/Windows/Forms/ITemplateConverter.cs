namespace Smart.Windows.Forms
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITemplateConverter<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="souece"></param>
        /// <returns></returns>
        T Convert(object souece);
    }
}
