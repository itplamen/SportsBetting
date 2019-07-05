namespace SportsBetting.Handlers.Commands.Categories
{
    using SportsBetting.Handlers.Commands.Contracts;

    public class CreateCategoryCommand : ICommand
    {
        public int Key { get; set; }

        public string Name { get; set; }

        public string SportId { get; set; }
    }
}