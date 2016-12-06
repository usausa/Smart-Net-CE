namespace Smart.Resolver.Metadatas
{
    using System.Collections.Generic;

    /// <summary>
    ///
    /// </summary>
    public class TypeMetadata
    {
        public IList<ConstructorMetadata> TargetConstructors { get; private set; }

        public IList<PropertyMetadata> TargetProperties { get; private set; }

        public TypeMetadata(IList<ConstructorMetadata> targetConstructors, IList<PropertyMetadata> targetProperties)
        {
            TargetConstructors = targetConstructors;
            TargetProperties = targetProperties;
        }
    }
}
