using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Models
{
    public class carditem
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public decimal total { get { return quantity * price; } }
        public string image { get; set; }

        public carditem()
        {
        }
        public carditem(product product)
        {
            productid = product.id;
            productname = product.name;
            price = product.price;
            quantity = 1;
            image = product.image;
                
        }
    }
   
}
