namespace SportsBetting.IoCContainer
{
    using SimpleInjector;
    using SimpleInjector.Packaging;

    public static class SportsBettingContainer
    {
        private readonly static Container container = new Container();

        public static void Initialize(IPackage package)
        {
            package.RegisterServices(container);

            container.Verify();
        }

        public static T Resolve<T>()
            where T : class
        {
            return container.GetInstance<T>();
        }
    }
}