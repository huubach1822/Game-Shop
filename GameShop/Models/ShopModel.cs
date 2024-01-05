using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameShop.Models
{
    public class ShopModel
    {     
            public List<product> productList { get; set; }
            public List<category> categoryList { get; set; }
            public int maximumPrice { get; set; }
        
    }
}