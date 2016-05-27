namespace Smart.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    internal class MemberInformation
    {
        public IAccessor Accessor { get; private set; }

        public bool IsPublic { get; private set; }

        public MemberInformation(IAccessor accessor, bool isPublic)
        {
            Accessor = accessor;
            IsPublic = isPublic;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class TypeInformation
    {
        private readonly Dictionary<string, MemberInformation> memberMap = new Dictionary<string, MemberInformation>();

        public Type MemberType { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        public TypeInformation(Type type)
        {
            MemberType = type;
            BuildMember(type);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        private void BuildMember(Type type)
        {
            foreach (var mi in type.GetPublicAccessableMember())
            {
                memberMap[mi.Name] = new MemberInformation(mi.ToAccessor(), true);
            }

            foreach (var mi in type.GetPrivateAccessableMember())
            {
                memberMap[mi.Name] = new MemberInformation(mi.ToAccessor(), false);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetAccessors()
        {
            return GetAccessors(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetAccessors(bool nonPublic)
        {
            return memberMap.Values.Where(c => nonPublic || c.IsPublic).Select(c => c.Accessor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetReadableAccessors()
        {
            return GetReadableAccessors(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetReadableAccessors(bool nonPublic)
        {
            return memberMap.Values.Where(c => (nonPublic || c.IsPublic) && c.Accessor.CanRead).Select(c => c.Accessor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetWritableAccessors()
        {
            return GetWritableAccessors(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public IEnumerable<IAccessor> GetWritableAccessors(bool nonPublic)
        {
            return memberMap.Values.Where(c => (nonPublic || c.IsPublic) && c.Accessor.CanWrite).Select(c => c.Accessor);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IAccessor GetAccessor(string name)
        {
            return GetAccessor(name, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="nonPublic"></param>
        /// <returns></returns>
        public IAccessor GetAccessor(string name, bool nonPublic)
        {
            MemberInformation memberInformation;
            if (!memberMap.TryGetValue(name, out memberInformation))
            {
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Member {0} is not exist.", name));
            }
            if (!nonPublic && !memberInformation.IsPublic)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Member {0} is not public.", name));
            }
            return memberInformation.Accessor;
        }
    }
}
