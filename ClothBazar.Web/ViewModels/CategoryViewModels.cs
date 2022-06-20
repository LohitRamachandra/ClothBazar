using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class CategoryViewModels
    {
        public class NewCategoryViewModel
        {
            //[Required]
            //[MinLength(5), MaxLength(50)]
            public string Name { get; set; }

            //[MaxLength(500)]
            public string Description { get; set; }

            public decimal Price { get; set; }

            public int CategoryId { get; set; }
        }

    }
}