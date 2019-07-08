namespace SportsBetting.Services.Data.Contracts
{
    using System.Collections.Generic;

    using SportsBetting.Data.Models;

    public interface ICategoriesService
    {
        IEnumerable<Category> Get(IEnumerable<string> categoryIds);
    }
}