namespace SportsBetting.Services.Data.Contracts
{
    using SportsBetting.Data.Models;

    public interface ICategoriesService
    {
        string Add(string name, string sportId);

        Category Get(string name);
    }
}