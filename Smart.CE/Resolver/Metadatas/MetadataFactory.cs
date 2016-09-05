namespace Smart.Resolver.Metadatas
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Smart.Reflection;
    using Smart.Resolver.Attributes;
    using Smart.Resolver.Constraints;

    /// <summary>
    ///
    /// </summary>
    public class MetadataFactory : IMetadataFactory
    {
        private readonly Dictionary<Type, TypeMetadata> metadatas = new Dictionary<Type, TypeMetadata>();

        public ISelector Selector { get; set; }

        /// <summary>
        ///
        /// </summary>
        public MetadataFactory()
        {
            Selector = new Selector();
        }

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

                var constructor = Selector.SelectConstructor(type);
                    var constructorConstraint = constructor == null ? null : constructor.GetParameters()
                    .Select(_ => CreateConstraint((ConstraintAttribute[])Attribute.GetCustomAttributes(_, typeof(ConstraintAttribute))))
                    .ToList();

                var properties = Selector.SelectProperties(type)
                    .Select(_ => new PropertyMetadata(_.ToAccessor(), CreateConstraint((ConstraintAttribute[])Attribute.GetCustomAttributes(_, typeof(ConstraintAttribute)))))
                    .ToList();

                metadata = new TypeMetadata(new ConstructorMetadata(constructor, constructorConstraint), properties);

                metadatas[type] = metadata;

                return metadata;
            }
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
