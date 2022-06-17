﻿using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductsService _productsService = new ProductsService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProductTable
        public ActionResult ProductTable(string search)
        {
            var products = _productsService.GetProducts();
            if (string.IsNullOrEmpty(search) == false)
            {
                products = products.Where(p => p.Name != null && p.Name.Contains(search)).ToList();
            }
            
            return PartialView(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // Post: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            _productsService.SaveProduct(product);
            return RedirectToAction("ProductTable");
        }

        // GET: Product/Edit/{id}
        public ActionResult Edit(int id)
        {
            var product = _productsService.GetProduct(id);
            return PartialView(product);
        }

        // Post: Product/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            _productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }

        //// GET: Product/Delete/{id}
        //public ActionResult Delete(int id)
        //{
        //    var product = _productsService.GetProduct(id);
        //    return PartialView(product);
        //}

        // Post: Product/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _productsService.DeleteProduct(id);
            return RedirectToAction("ProductTable");
        }
    }
}