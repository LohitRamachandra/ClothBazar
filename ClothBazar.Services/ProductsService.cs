using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ClothBazar.Database;
using ClothBazar.Entities;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        
        public Product GetProduct(int id)
        {
            using (var context = new CBContext())
            {
                return context.Products.Find(id);
            }
        }
        public List<Product> GetProducts()
        {
            //var context = new CBContext();
            //return context.Products.Include(x => x.Category).ToList();
            using (var context = new CBContext())
            {
                return context.Products.Include(x => x.Category).ToList();
                //return context.Products.Include("Category").ToList();
                //return context.Products.Include("Category.Product").ToList();
            }
        }
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var context = new CBContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
