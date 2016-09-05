namespace Smart.Resolver.Metadatas
{
    using Smart.Reflection;
    using Smart.Resolver.Constraints;

    /// <summary>
    ///
    /// </summary>
    public class PropertyMetadata
    {
        public IAccessor Accessor { get; private set; }

        public IConstraint Constraint { get; private set; }

        public PropertyMetadata(IAccessor accessor, IConstraint constraint)
        {
            Accessor = accessor;
            Constraint = constraint;
        }
    }
}
