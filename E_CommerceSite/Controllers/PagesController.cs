using E_CommerceSite.infrestracuter;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Controllers
{
    public class PagesController : Controller
    {
        private readonly Ecommersitecontext db;
       

        public PagesController(Ecommersitecontext db)
        {
            this.db = db;
            
        }

        public async Task <IActionResult> Pages(string slug)
        {
            if (slug == null)
            {
                return View( db.page.Where(x => x.slug == "home").FirstOrDefault());
            }
            pages page = db.page.Where(x => x.slug == slug).FirstOrDefault();
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
    }
}
