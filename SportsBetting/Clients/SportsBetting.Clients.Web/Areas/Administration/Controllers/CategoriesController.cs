namespace SportsBetting.Clients.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using SportsBetting.Clients.Web.Areas.Administration.Models.Categories;
    using SportsBetting.Common.Contracts;
    using SportsBetting.Data.Models;
    using SportsBetting.Services.Data.Contracts;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IMapper<Category, CategoryViewModel> mapper;

        public CategoriesController(ICategoriesService categoriesService, IMapper<Category, CategoryViewModel> mapper)
        {
            this.categoriesService = categoriesService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            IEnumerable<Category> categories = categoriesService.All();
            IEnumerable<CategoryViewModel> categoryViewModels = mapper.Map(categories);

            return View(categoryViewModels);
        }
    }
}