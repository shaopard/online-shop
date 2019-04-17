using onLineShop.DAL.Data;
using onLineShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onLineShop.DAL.Repositories
{
    public class ShoppingCartRepository : RepositoryBase<ShoppingCart>
    {
        public ShoppingCartRepository(DataContext context) : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
