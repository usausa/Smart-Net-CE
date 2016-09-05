namespace Smart.Resolver.Activators
{
    /// <summary>
    ///
    /// </summary>
    public class InitializeActivator : IActivator
    {
        public void Activate(object instance)
        {
            var initializable = instance as IInitializable;
            if (initializable != null)
            {
                initializable.Initialize();
            }
        }
    }
}
