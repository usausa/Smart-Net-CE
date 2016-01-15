namespace Smart
{
    using System;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class EnumEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetName(Type type, object value)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if (type.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("type must be Enum.", "type");
            }

            foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                object numericValue;

                try
                {
                    numericValue = Convert.ChangeType(fieldInfo.GetValue(null), value.GetType(), null);
                }
                catch
                {
                    throw new ArgumentException("value must be enum base type.", "value");
                }

                if (numericValue == value)
                {
                    return fieldInfo.Name;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[] GetNames(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("type must be Enum.", "type");
            }

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            var names = new string[fields.Length];
            for (var i = 0; i < fields.Length; i++)
            {
                names[i] = fields[i].Name;
            }

            return names;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Enum[] GetValues(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type.BaseType != Type.GetType("System.Enum"))
            {
                throw new ArgumentException("type must be Enum.", "type");
            }

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            var values = new Enum[fields.Length];
            for (var i = 0; i < fields.Length; i++)
            {
                values[i] = (Enum)fields[i].GetValue(null);
            }

            return values;
        }
    }
}
