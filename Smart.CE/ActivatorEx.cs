namespace Smart
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    public static class ActivatorEx
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CreateInstance(Type type, params object[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            if (args.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (type.IsAbstract || type.IsInterface)
            {
                throw new MemberAccessException();
            }

            var types = new Type[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                if (args[i] != null)
                {
                    types[i] = args[i].GetType();
                }
            }

            foreach (var info in type.GetConstructors(BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default))
            {
                var parameters = info.GetParameters();
                if (parameters.Length != types.Length)
                {
                    continue;
                }

                if (!parameters.Where((p, i) => (types[i] != null) && (!p.ParameterType.IsAssignableFrom(types[i]))).Any())
                {
                    return info.Invoke(args);
                }
            }

            throw new MissingMethodException();
        }
    }
}
