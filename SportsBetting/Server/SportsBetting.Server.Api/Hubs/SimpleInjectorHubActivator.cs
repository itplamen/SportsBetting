namespace SportsBetting.Server.Api.Hubs
{
    using Microsoft.AspNet.SignalR.Hubs;

    using SimpleInjector;

    public class SimpleInjectorHubActivator : IHubActivator
    {
        private readonly Container container;

        public SimpleInjectorHubActivator(Container container)
        {
            this.container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)container.GetInstance(descriptor.HubType);
        }
    }
}