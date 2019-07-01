namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ICategoriesService
    {
        string Add(string name, string sportId);

        Category Get(string name);

        IEnumerable<Category> All();
    }
}