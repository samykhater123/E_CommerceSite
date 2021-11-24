using E_CommerceSite.infrestracuter;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class pagesController : Controller
    {

        private readonly Ecommersitecontext db;

        public pagesController(Ecommersitecontext db)
        {
           this.db = db;
        }

        public async Task <IActionResult> Index()
        {
            IQueryable<pages> page = from p in db.page orderby p.sorting select p;

            List<pages> pagelist = await page.ToListAsync();

            return View(pagelist);
        }

        public async Task<IActionResult> Details(int id)
        {
            pages pag=await db.page.FirstOrDefaultAsync(c => c.id == id);

            if (pag == null)
            {
                return NotFound();
            }

            return View(pag);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(pages bage)
        {


            if (ModelState.IsValid)
            {
                bage.slug = bage.titel.ToLower().Replace(" ", "-");
                bage.sorting = 100;
               // pages.Content = bage.contant;
                var slug = await db.page.FirstOrDefaultAsync(x => x.slug == bage.slug);
                if (slug != null)
                {
                    ModelState.AddModelError("","title is exostes");
                    return View(bage);
                }

                db.Add(bage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

           

            return View(bage);
        }
    }
}
