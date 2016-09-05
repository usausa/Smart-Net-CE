﻿namespace Smart.Resolver.Activators
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public class ActivatePipeline : IActivatePipeline
    {
        /// <summary>
        ///
        /// </summary>
        public IList<IActivator> Activators { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="activators"></param>
        public ActivatePipeline(params IActivator[] activators)
        {
            Activators = activators.ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="instance"></param>
        public void Activate(object instance)
        {
            foreach (var activator in Activators)
            {
                activator.Activate(instance);
            }
        }
    }
}
