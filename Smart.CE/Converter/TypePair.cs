namespace Smart.Converter
{
    using System;

    /// <summary>
    ///
    /// </summary>
    public struct TypePair : IEquatable<TypePair>
    {
        private readonly int hashCode;

        private readonly Type sourceType;

        private readonly Type targetType;

        /// <summary>
        ///
        /// </summary>
        public Type SourceType
        {
            get { return sourceType; }
        }

        /// <summary>
        ///
        /// </summary>
        public Type TargetType
        {
            get { return targetType; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public TypePair(Type sourceType, Type targetType)
        {
            this.sourceType = sourceType;
            this.targetType = targetType;
            hashCode = (sourceType.GetHashCode() << 5) ^ targetType.GetHashCode();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TypePair left, TypePair right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TypePair left, TypePair right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TypePair other)
        {
            return SourceType == other.SourceType && TargetType == other.TargetType;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && obj.GetType() == GetType() && Equals((TypePair)obj);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
