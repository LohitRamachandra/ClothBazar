using ClothBazar.Database;
using ClothBazar.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClothBazar.Services
{
    public class CategoriesService
    {
        public Category GetCategory(int id)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(id);
            }
        }
        public List<Category> GetCategories()
        {

            using (var context = new CBContext())
            {
                 return context.Categories.ToList();
            }
        }
        public void SaveCategory(Category category)
        {
            using(var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteCategory(int id)
        {
            using (var context = new CBContext())
            {
                //context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var category = context.Categories.Find(id);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
