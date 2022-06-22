using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductsService _productsService = new ProductsService();

        public CategoriesService _categoriesService = new CategoriesService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        //// GET: ProductTable
        //public ActionResult ProductTable(string search)
        //{
        //    var products = _productsService.GetProducts();
        //    if (string.IsNullOrEmpty(search) == false)
        //    {
        //        products = products.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
        //    }

        //    return PartialView(products);
        //}

        public ActionResult ProductTable(string search, int? pageNo)
        {
            int pageSize = 5;
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = _productsService.GetProductsCount(search);
            model.Products = _productsService.GetProducts(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }

        //// GET: Product/Create
        //public ActionResult Create()
        //{
        //    CategoriesService categoriesService = new CategoriesService();
        //    var categories = categoriesService.GetAllCategories();
        //    return PartialView(categories);
        //}

        //// Post: Product/Create
        //[HttpPost]
        //public ActionResult Create(NewCategoryViewModel model)
        //{
        //    CategoriesService categoriesService = new CategoriesService();

        //    var newProduct = new Product();
        //    newProduct.Name = model.Name;
        //    newProduct.Description = model.Description;
        //    newProduct.Price = model.Price;
        //    //newProduct.CategoryId = model.CategoryId;
        //    newProduct.Category = categoriesService.GetCategory(model.CategoryId);

        //    _productsService.SaveProduct(newProduct);
        //    return RedirectToAction("ProductTable");
        //}

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();
            model.AvailableCategories = _categoriesService.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {


            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = _categoriesService.GetCategory(model.CategoryId);
            newProduct.ImageURL = model.ImageURL;

            _productsService.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }

        //// GET: Product/Edit/{id}
        //public ActionResult Edit(int id)
        //{
        //    var product = _productsService.GetProduct(id);
        //    return PartialView(product);
        //}

        //// Post: Product/Edit/{id}
        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{
        //    _productsService.UpdateProduct(product);
        //    return RedirectToAction("ProductTable");
        //}

        //// GET: Product/Delete/{id}
        //public ActionResult Delete(int id)
        //{
        //    var product = _productsService.GetProduct(id);
        //    return PartialView(product);
        //}

        // GET: Product/Edit/{id}
        public ActionResult Edit(int id)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = _productsService.GetProduct(id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryId = product.Category != null ? product.Category.Id : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = _categoriesService.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = _productsService.GetProduct(model.Id);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryId = model.CategoryId;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }
            
            _productsService.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }


        // Post: Product/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productsService.DeleteProduct(id);
            return RedirectToAction("ProductTable");
        }
    }
}