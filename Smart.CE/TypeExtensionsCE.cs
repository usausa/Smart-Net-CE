namespace Smart
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///
    /// </summary>
    /// <param name="type"></param>
    /// <param name="filterCriteria"></param>
    /// <returns></returns>
    public delegate bool TypeFilter(Type type, object filterCriteria);

    /// <summary>
    ///
    /// </summary>
    /// <param name="memberInfo"></param>
    /// <param name="filterCriteria"></param>
    /// <returns></returns>
    public delegate bool MemberFilter(MemberInfo memberInfo, object filterCriteria);

    /// <summary>
    ///
    /// </summary>
    public static class TypeExtensionsCE
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static Type[] FindInterfaces(this Type type, TypeFilter filter, object filterCriteria)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            var interfaces = type.GetInterfaces();
            var count = 0;
            for (var i = 0; i < interfaces.Length; i++)
            {
                if (!filter(interfaces[i], filterCriteria))
                {
                    interfaces[i] = null;
                }
                else
                {
                    count++;
                }
            }

            if (count == interfaces.Length)
            {
                return interfaces;
            }

            var array = new Type[count];
            count = 0;
            foreach (var t in interfaces.Where(t => t != null))
            {
                array[count++] = t;
            }

            return array;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberType"></param>
        /// <param name="bindingAttr"></param>
        /// <param name="filter"></param>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Compatibility")]
        public static MemberInfo[] FindMembers(this Type type, MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
        {
            MethodInfo[] methods = null;
            ConstructorInfo[] constructors = null;
            FieldInfo[] fields = null;
            PropertyInfo[] properties = null;
            EventInfo[] events = null;
            Type[] nestedTypes = null;

            var count = 0;
            if ((memberType & MemberTypes.Method) != 0)
            {
                methods = type.GetMethods(bindingAttr);
                if (filter != null)
                {
                    for (var i = 0; i < methods.Length; i++)
                    {
                        if (!filter(methods[i], filterCriteria))
                        {
                            methods[i] = null;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count += methods.Length;
                }
            }

            if ((memberType & MemberTypes.Constructor) != 0)
            {
                constructors = type.GetConstructors(bindingAttr);
                if (filter != null)
                {
                    for (var i = 0; i < constructors.Length; i++)
                    {
                        if (!filter(constructors[i], filterCriteria))
                        {
                            constructors[i] = null;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count += constructors.Length;
                }
            }

            if ((memberType & MemberTypes.Field) != 0)
            {
                fields = type.GetFields(bindingAttr);
                if (filter != null)
                {
                    for (var i = 0; i < fields.Length; i++)
                    {
                        if (!filter(fields[i], filterCriteria))
                        {
                            fields[i] = null;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count += fields.Length;
                }
            }

            if ((memberType & MemberTypes.Property) != 0)
            {
                properties = type.GetProperties(bindingAttr);
                if (filter != null)
                {
                    for (var i = 0; i < properties.Length; i++)
                    {
                        if (!filter(properties[i], filterCriteria))
                        {
                            properties[i] = null;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count += properties.Length;
                }
            }

            if ((memberType & MemberTypes.Event) != 0)
            {
                events = type.GetEvents();
                if (filter != null)
                {
                    for (var i = 0; i < events.Length; i++)
                    {
                        if (!filter(events[i], filterCriteria))
                        {
                            events[i] = null;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count += events.Length;
                }
            }

            if ((memberType & MemberTypes.NestedType) != 0)
            {
                nestedTypes = type.GetNestedTypes(bindingAttr);
                if (filter != null)
                {
                    for (var i = 0; i < nestedTypes.Length; i++)
                    {
                        if (!filter(nestedTypes[i], filterCriteria))
                        {
                            nestedTypes[i] = null;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    count += nestedTypes.Length;
                }
            }

            var array = new MemberInfo[count];
            count = 0;

            if (methods != null)
            {
                foreach (var t in methods.Where(t => t != null))
                {
                    array[count++] = t;
                }
            }

            if (constructors != null)
            {
                foreach (var t in constructors.Where(t => t != null))
                {
                    array[count++] = t;
                }
            }

            if (fields != null)
            {
                foreach (var t in fields.Where(t => t != null))
                {
                    array[count++] = t;
                }
            }

            if (properties != null)
            {
                foreach (var t in properties.Where(t => t != null))
                {
                    array[count++] = t;
                }
            }

            if (events != null)
            {
                foreach (var t in events.Where(t => t != null))
                {
                    array[count++] = t;
                }
            }

            if (nestedTypes != null)
            {
                foreach (var t in nestedTypes.Where(t => t != null))
                {
                    array[count++] = t;
                }
            }

            return array;
        }
    }
}
