using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var categories = _categoriesService.GetCategories();
            return View(categories);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // Post: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            _categoriesService.SaveCategory(category);
            return View();
        }

        // GET: Category/Edit/{id}
        public ActionResult Edit(int id)
        {
            var category = _categoriesService.GetCategory(id);
            return View(category);
        }

        // Post: Category/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            _categoriesService.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        // GET: Category/Delete/{id}
        public ActionResult Delete(int id)
        {
            var category = _categoriesService.GetCategory(id);
            return View(category);
        }

        // Post: Category/Delete/{id}
        [HttpPost]
        public ActionResult Delete(Category category)
        {
             category = _categoriesService.GetCategory(category.Id);
            _categoriesService.DeleteCategory(category.Id);
            return RedirectToAction("Index");
        }
    }
}