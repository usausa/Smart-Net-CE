namespace Smart.Resolver.Metadatas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Smart.Reflection;
    using Smart.Resolver.Attributes;
    using Smart.Resolver.Constraints;

    /// <summary>
    ///
    /// </summary>
    public class MetadataFactory : IMetadataFactory
    {
        private static readonly Type InjectType = typeof(InjectAttribute);

        private static readonly Type EnumerableType = typeof(IEnumerable<>);

        private static readonly Type CollectionType = typeof(ICollection<>);

        private static readonly Type ListType = typeof(IList<>);

        private readonly Dictionary<Type, TypeMetadata> metadatas = new Dictionary<Type, TypeMetadata>();

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public TypeMetadata GetMetadata(Type type)
        {
            lock (metadatas)
            {
                TypeMetadata metadata;
                if (metadatas.TryGetValue(type, out metadata))
                {
                    return metadata;
                }

                var constructors = type.GetConstructors()
                    .OrderByDescending(c => c.IsDefined(InjectType, true) ? 1 : 0)
                    .ThenByDescending(c => c.GetParameters().Length)
                    .Select(c => CreateConstructorMetadata(c))
                    .ToArray();

                var properties = type.GetProperties()
                    .Where(p => p.IsDefined(InjectType, true))
                    .Select(p => CreatePropertyMetadata(p))
                    .ToArray();

                metadata = new TypeMetadata(constructors, properties);

                metadatas[type] = metadata;

                return metadata;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        private static ConstructorMetadata CreateConstructorMetadata(ConstructorInfo ci)
        {
            var parameters = ci.GetParameters()
                .Select(p => CreateParameterMetadata(p))
                .ToArray();

            var constraints = ci.GetParameters()
                .Select(p => CreateConstraint((ConstraintAttribute[])Attribute.GetCustomAttributes(p, typeof(ConstraintAttribute))))
                .ToArray();

            return new ConstructorMetadata(ci, parameters, constraints);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private static ParameterMetadata CreateParameterMetadata(ParameterInfo pi)
        {
            // Array
            if (pi.ParameterType.IsArray)
            {
                return new ParameterMetadata(pi, pi.ParameterType.GetElementType());
            }

            // IEnumerable type
            if (pi.ParameterType.IsGenericType)
            {
                var genericType = pi.ParameterType.GetGenericTypeDefinition();
                if ((genericType == EnumerableType) || (genericType == CollectionType) || (genericType == ListType))
                {
                    return new ParameterMetadata(pi, pi.ParameterType.GetGenericArguments()[0]);
                }
            }

            return new ParameterMetadata(pi, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        private static PropertyMetadata CreatePropertyMetadata(PropertyInfo pi)
        {
            return new PropertyMetadata(pi.ToAccessor(), CreateConstraint((ConstraintAttribute[])Attribute.GetCustomAttributes(pi, typeof(ConstraintAttribute))));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        private static IConstraint CreateConstraint(IEnumerable<ConstraintAttribute> attributes)
        {
            var constraints = attributes
                .Select(a => a.CreateConstraint())
                .ToArray();

            if (constraints.Length == 0)
            {
                return null;
            }

            if (constraints.Length == 1)
            {
                return constraints[0];
            }

            return new ChainConstraint(constraints);
        }
    }
}
