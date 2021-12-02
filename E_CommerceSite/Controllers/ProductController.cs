using E_CommerceSite.infrestracuter;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly Ecommersitecontext db;

        public ProductController(Ecommersitecontext db)
        {
            this.db = db;
        }



        public IActionResult Index(int p = 1)
        {
            int pagesize = 6;
            var products = db.products.OrderByDescending(x => x.id)
                                                     
                                                      .Skip((p - 1) * pagesize)
                                                      .Take(pagesize);

            ViewBag.pagenumber = p;
            ViewBag.pagerange = pagesize;
           
            ViewBag.totalpages = (int)Math.Ceiling((decimal)(db.products.Count() / pagesize+1));
            

            return View(products.ToList());
        }


        public IActionResult ProductbyCategory(string categoryslug, int p = 1)
        {
            Category categ = db.categories.Where(x => x.slug == categoryslug).FirstOrDefault();

            if(categ == null)
            {
               return RedirectToAction("Index");
            }
            int pagesize = 6;
            var products = db.products.OrderByDescending(x => x.id)
                                                      .Where(z => z.categoryid==categ.id)
                                                      .Skip((p - 1) * pagesize)
                                                      .Take(pagesize);

            ViewBag.pagenumber = p;
            ViewBag.pagerange  = pagesize;
           
            ViewBag.totalpages = (int)Math.Ceiling((decimal)db.products.Where(z => z.categoryid == categ.id).Count() / pagesize);

            ViewBag.categoryname = categ.name;
            return View(products.ToList());
        }
    }
}
