using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ClothBazar.Entities;

namespace ClothBazar.Web.ViewModels
{
   
        public class CategorySearchViewModel
        {
            public List<Category> Categories { get; set; }
            public string SearchTerm { get; set; }

            public Pager Pager { get; set; }
        }
        public class NewCategoryViewModel
        {
            //[Required]
            //[MinLength(5), MaxLength(50)]
            public string Name { get; set; }

            //[MaxLength(500)]
            public string Description { get; set; }

            public decimal Price { get; set; }

            public int CategoryId { get; set; }
            public string ImageURL { get; set; }

            public bool isFeatured { get; set; }
        }

        public class EditCategoryViewModel
        {
            public int Id { get; set; }

            public string Name { get; set; }
            public string Description { get; set; }

            public string ImageURL { get; set; }

            public bool isFeatured { get; set; }
        }


}