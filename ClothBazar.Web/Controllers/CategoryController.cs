using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class CategoryController : Controller
    {
       
        public CategoriesService _categoriesService = new CategoriesService();
        //public CategoryController(CategoriesService categoriesService)
        //{
        //    _categoriesService = categoriesService;
        //}

        // GET: Category
        [HttpGet]
        public ActionResult Index()
        {
            //var categories = _categoriesService.GetCategories();
            //return View(categories);
            return View();
        }

        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = _categoriesService.GetCategoriesCount(search);
            model.Categories = _categoriesService.GetCategories(search, pageNo.Value);

            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 3);

                return PartialView("CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        //// GET: Category/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// Post: Category/Create
        //[HttpPost]
        //public ActionResult Create(Category category)
        //{
        //    _categoriesService.SaveCategory(category);
        //    return View();
        //}

        #region Creation

        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.isFeatured = model.isFeatured;

                _categoriesService.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        #endregion

        //// GET: Category/Edit/{id}
        //public ActionResult Edit(int id)
        //{
        //    var category = _categoriesService.GetCategory(id);
        //    return View(category);
        //}

        //// Post: Category/Edit/{id}
        //[HttpPost]
        //public ActionResult Edit(Category category)
        //{
        //    _categoriesService.UpdateCategory(category);
        //    return RedirectToAction("Index");
        //}

        #region Updation

        [HttpGet]
        public ActionResult Edit(int id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = _categoriesService.GetCategory(id);

            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = _categoriesService.GetCategory(model.Id);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            _categoriesService.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }

        #endregion

        //// GET: Category/Delete/{id}
        //public ActionResult Delete(int id)
        //{
        //    var category = _categoriesService.GetCategory(id);
        //    return View(category);
        //}

        //// Post: Category/Delete/{id}
        //[HttpPost]
        //public ActionResult Delete(Category category)
        //{
        //     category = _categoriesService.GetCategory(category.Id);
        //    _categoriesService.DeleteCategory(category.Id);
        //    return RedirectToAction("Index");
        //}


        [HttpPost]
        public ActionResult Delete(int ID)
        {
            _categoriesService.DeleteCategory(ID);

            return RedirectToAction("CategoryTable");
        }
    }
}