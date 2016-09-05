namespace Smart.Resolver.Metadatas
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class TypeMetadata
    {
        public ConstructorMetadata TargetConstructor { get; private set; }

        public IList<PropertyMetadata> TargetProperties { get; private set; }

        public TypeMetadata(ConstructorMetadata targetConstructor, IList<PropertyMetadata> targetProperties)
        {
            TargetConstructor = targetConstructor;
            TargetProperties = targetProperties;
        }
    }
}
