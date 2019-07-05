namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ICategoriesService
    {
        Category Get(string name);

        IEnumerable<Category> Get(IEnumerable<string> categoryIds);

        IEnumerable<Category> AllWithDeleted();
    }
}