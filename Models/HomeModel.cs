using ASP.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.Models
{
    public class HomeModel
    {
        public List<Product> lstProduct { get; set; }
        public List<Category> lstCategory { get; set; }
    }
}