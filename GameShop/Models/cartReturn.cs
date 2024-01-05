using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameShop.Models
{
    public class cartReturn
    {
        public List<product> list { get; set; }
        public int total { get; set; }
    }
}