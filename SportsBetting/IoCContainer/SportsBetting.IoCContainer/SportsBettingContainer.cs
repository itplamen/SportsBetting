namespace SportsBetting.IoCContainer
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    public static class SportsBettingContainer
    {
        private readonly static Container container;

        static SportsBettingContainer()
        {
            container = new Container();
            container.Options.DefaultLifestyle = Lifestyle.Singleton;
        }

        public static void Initialize(IPackage[] packages)
        {
            foreach (var package in packages)
            {
                package.RegisterServices(container);
            }

            container.Verify();
        }

        public static T Resolve<T>()
            where T : class
        {
            return container.GetInstance<T>();
        }
    }
}