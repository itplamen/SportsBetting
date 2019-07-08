namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Categories;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Queries.Contracts;
    using SportsBetting.Services.Data.Contracts;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IQueryHandler<IEnumerable<Category>> allWithDeletedHandler;

        public CategoriesController(ICategoriesService categoriesService, IQueryHandler<IEnumerable<Category>> allWithDeletedHandler)
        {
            this.categoriesService = categoriesService;
            this.allWithDeletedHandler = allWithDeletedHandler;
        }

        public ActionResult Index()
        {
            IEnumerable<Category> categories = allWithDeletedHandler.Handle();
            IEnumerable<CategoryViewModel> categoryViewModels = Mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            return View(categoryViewModels);
        }
    }
}