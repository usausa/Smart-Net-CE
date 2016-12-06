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
                    .OrderByDescending(_ => _.IsDefined(InjectType, true) ? 1 : 0)
                    .ThenByDescending(_ => _.GetParameters().Length)
                    .Select(_ => CreateConstructorMetadata(_))
                    .ToList();

                var properties = type.GetProperties()
                    .Where(_ => _.IsDefined(InjectType, true))
                    .Select(_ => CreatePropertyMetadata(_))
                    .ToList();

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
            var constraints = ci.GetParameters()
                .Select(_ => CreateConstraint((ConstraintAttribute[])Attribute.GetCustomAttributes(_, typeof(ConstraintAttribute))))
                .ToList();

            return new ConstructorMetadata(ci, constraints);
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
                .Select(_ => _.CreateConstraint())
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
