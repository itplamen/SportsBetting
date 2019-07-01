namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using SportsBetting.Clients.Web.Areas.Administration.Mappers;
    using SportsBetting.Clients.Web.Areas.Administration.Models.Categories;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public ActionResult Index()
        {
            IEnumerable<Category> categories = categoriesService.All();
            IEnumerable<CategoryViewModel> categoryViewModels = CategoriesMapper.Map(categories);

            return View(categoryViewModels);
        }
    }
}