namespace Smart.Windows.Forms
{
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class ConcatConverter : Component, ITemplateConverter<string>
    {
        public string Properties { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Convert(object source)
        {
            if (source == null)
            {
                return string.Empty;
            }

            var type = source.GetType();

            var sb = new StringBuilder();
            foreach (var accessor in Properties.Split(',').Select(x => TemplateAccessorCache.GetAccessor(type, x.Trim())).Where(x => (x != null) && x.CanRead))
            {
                sb.Append(accessor.GetValue(source));
            }

            return sb.ToString();
        }
    }
}
