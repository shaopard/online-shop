using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onLineShop.Model
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<CartItem> ShoppingCartItems { get; set; }
    }
}
