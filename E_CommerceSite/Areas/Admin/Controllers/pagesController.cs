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

                TempData["success"] = "the page has been add";

                return RedirectToAction("Index");
            }

           

            return View(bage);
        }

        public async Task<IActionResult> Edit(int id)
        {
            pages pag = await db.page.FindAsync(id);

            if (pag == null)
            {
                return NotFound();
            }

            return View(pag);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(pages bage)
        {


            if (ModelState.IsValid)
            {
                bage.slug = bage.id == 1 ? "home" : bage.titel.ToLower().Replace(" ", "-"); 
                
                
                var slug = await db.page.Where(p=>p.id !=bage.id).FirstOrDefaultAsync(x => x.slug == bage.slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "page is exostes");
                    return View(bage);
                }

                db.Update(bage);   
                await db.SaveChangesAsync();

                TempData["success"] = "the page has been edited";

                return RedirectToAction("Edit",new { id=bage.id});
            }



            return View(bage);
        }

        public async Task<IActionResult> Delete(int id)
        {
            pages pag = await db.page.FindAsync(id);

            db.page.RemoveRange();

            if (pag == null)
            {
                TempData["Error"] = "the page dosent existe";
            }
            else
            {
                db.page.Remove(pag);
                await db.SaveChangesAsync();
                TempData["success"] = "the page has been removed";
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> reorder(int[] id)
        {
            int count = 1;
            foreach (var pageid in id)
            {
                pages page = await db.page.FindAsync(pageid);
                page.sorting = count;
                db.Update(page);
                await db.SaveChangesAsync();
                count++;

            }
            return Ok();
        }

    }
}
