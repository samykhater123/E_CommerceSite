using E_CommerceSite.infrestracuter;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace E_CommerceSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly Ecommersitecontext db;

        public CategoryController(Ecommersitecontext db)
        {
            this.db = db;
        }
        


        public async Task<IActionResult> Index()
        {
           
            return View( db.categories.OrderBy(i=>i.sorting).ToList() );
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Category cat)
        {


            if (ModelState.IsValid)
            {
                cat.slug = cat.name.ToLower().Replace(" ", "-");
                cat.sorting = 100;
                // pages.Content = bage.contant;
                var slug = await db.categories.FirstOrDefaultAsync(x => x.slug == cat.slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "category is exostes");
                    return View(cat);
                }

                db.Add(cat);
                await db.SaveChangesAsync();

                TempData["success"] = "the category has been add";

                return RedirectToAction("Index");
            }



            return View(cat);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Category cat = await db.categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category cat)
        {


            if (ModelState.IsValid)
            {
                
                cat.slug = cat.id == 1 ? "home" : cat.name.ToLower().Replace(" ", "-");

                var slug = await db.categories.Where(p => p.id != cat.id).FirstOrDefaultAsync(x => x.slug == cat.slug);
                
                if (slug != null)
                {
                    ModelState.AddModelError("", "category is exostes");
                    return View(cat);
                }

                db.Update(cat);
                await db.SaveChangesAsync();

                TempData["success"] = "the category has been updated";

                return RedirectToAction("Edit", new { id = cat.id });
               
            }



            return View(cat);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Category pag = await db.categories.FindAsync(id);

            db.categories.RemoveRange();

            if (pag == null)
            {
                TempData["Error"] = "the Category dosent existe";
            }
            else
            {
                db.categories.Remove(pag);
                await db.SaveChangesAsync();
                TempData["success"] = "the Category has been removed";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> reorder(int[] id)
        {
            int count = 1;
            foreach (var catid in id)
            {
                Category cat = await db.categories.FindAsync(catid);
                cat.sorting = count;
                db.Update(cat);
                await db.SaveChangesAsync();
                count++;

            }
            return Ok();
        }
    }
}
