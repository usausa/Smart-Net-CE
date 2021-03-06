﻿namespace Smart.Resolver.Providers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Smart.ComponentModel;
    using Smart.Reflection;
    using Smart.Resolver.Bindings;
    using Smart.Resolver.Injectors;
    using Smart.Resolver.Metadatas;

    /// <summary>
    ///
    /// </summary>
    public class StandardProvider : IProvider
    {
        private readonly IInjector[] injectors;

        private readonly TypeMetadata metadata;

        private volatile ConstructorMetadata constructor;

        private volatile IActivator activator;

        /// <summary>
        ///
        /// </summary>
        public Type TargetType { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type"></param>
        /// <param name="components"></param>
        public StandardProvider(Type type, IComponentContainer components)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (components == null)
            {
                throw new ArgumentNullException("components");
            }

            TargetType = type;
            injectors = components.GetAll<IInjector>().ToArray();
            metadata = components.Get<IMetadataFactory>().GetMetadata(TargetType);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Framework only")]
        public object Create(IKernel kernel, IBinding binding)
        {
#pragma warning disable 420
            if (constructor == null)
            {
                Interlocked.CompareExchange(ref constructor, FindBestConstructor(kernel, binding), null);
                Interlocked.CompareExchange(ref activator, ActivatorCache.GetActivator(constructor.Constructor), null);
            }
#pragma warning restore 420

            var arguments = ResolveParameters(kernel, binding, constructor);
            var instance = activator.Create(arguments);

            for (var j = 0; j < injectors.Length; j++)
            {
                injectors[j].Inject(kernel, binding, metadata, instance);
            }

            return instance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        private ConstructorMetadata FindBestConstructor(IKernel kernel, IBinding binding)
        {
            if (metadata.TargetConstructors.Length == 0)
            {
                throw new InvalidOperationException(
                    String.Format(CultureInfo.InvariantCulture, "No constructor avaiable. type = {0}", TargetType.Name));
            }

            for (var i = 0; i < metadata.TargetConstructors.Length; i++)
            {
                var match = true;
                var cm = metadata.TargetConstructors[i];

                var parameters = cm.Parameters;
                for (var j = 0; j < parameters.Length; j++)
                {
                    var parameter = parameters[j];
                    var pi = parameter.Parameter;

                    // Constructor argument
                    if (binding.ConstructorArguments.GetParameter(pi.Name) != null)
                    {
                        continue;
                    }

                    // Multiple
                    if (parameter.ElementType != null)
                    {
                        continue;
                    }

                    // Resolve
                    if (kernel.CanResolve(pi.ParameterType, cm.Constraints[j]))
                    {
                        continue;
                    }

                    match = false;
                    break;
                }

                if (match)
                {
                    return cm;
                }
            }

            throw new InvalidOperationException(
                String.Format(CultureInfo.InvariantCulture, "Constructor parameter unresolved. type = {0}", TargetType.Name));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="binding"></param>
        /// <param name="constructor"></param>
        /// <returns></returns>
        private static object[] ResolveParameters(IKernel kernel, IBinding binding, ConstructorMetadata constructor)
        {
            var parameters = constructor.Parameters;
            if (parameters.Length == 0)
            {
                return null;
            }

            var arguments = new object[parameters.Length];
            for (var i = 0; i < arguments.Length; i++)
            {
                var parameter = parameters[i];
                var pi = parameter.Parameter;

                // Constructor argument
                var argument = binding.ConstructorArguments.GetParameter(pi.Name);
                if (argument != null)
                {
                    arguments[i] = argument.Resolve(kernel);
                    continue;
                }

                // Multiple
                if (parameter.ElementType != null)
                {
                    arguments[i] = ResolverHelper.ConvertArray(parameter.ElementType, kernel.ResolveAll(parameter.ElementType, constructor.Constraints[i]));
                    continue;
                }

                // Resolve
                bool resolve;
                var obj = kernel.TryResolve(pi.ParameterType, constructor.Constraints[i], out resolve);
                if (resolve)
                {
                    arguments[i] = obj;
                }
            }

            return arguments;
        }
    }
}
