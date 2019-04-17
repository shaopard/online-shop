using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onLineShop.Model
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }

        [Required, MaxLength(255)]
        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
