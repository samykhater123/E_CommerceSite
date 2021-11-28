using E_CommerceSite.infrestracuter;
using E_CommerceSite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly Ecommersitecontext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(Ecommersitecontext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int p=1)
        {
            int pagesize = 5;
            var products = db.products.OrderByDescending(x => x.id)
                                                      .Include(z => z.category)
                                                      .Skip((p-1)*pagesize)
                                                      .Take(pagesize);

            ViewBag.pagenumber = p;
            ViewBag.pagerange = pagesize;
            if((decimal)(db.products.Count() % pagesize) > 0)
            {
                ViewBag.totalpages = (int)Math.Ceiling((decimal)(db.products.Count() / pagesize) + 1);
            }
            else
            {
                ViewBag.totalpages = (int)Math.Ceiling((decimal)(db.products.Count() / pagesize) );
            }
            
            return View(products.ToList());
        }

        public IActionResult Create()
        {
            ViewBag.category = new SelectList(db.categories.OrderBy(c => c.sorting), "id", "name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(product prod)
        {
            ViewBag.category = new SelectList(db.categories.OrderBy(c => c.sorting), "id", "name");

            if (ModelState.IsValid)
            {
                prod.slug = prod.name.ToLower().Replace(" ", "-");
               
                var slug = await db.products.FirstOrDefaultAsync(x => x.slug == prod.slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "product is exostes");
                    return View(prod);
                }

                string imageName = "cc.jpg";

                if(prod.imageuploade != null)
                {
                    string uploaddir = Path.Combine(webHostEnvironment.WebRootPath, "madia/product");
                    imageName = Guid.NewGuid().ToString()+"_"+prod.imageuploade.FileName;
                    string filepath = Path.Combine(uploaddir, imageName);
                    FileStream fs = new FileStream(filepath, FileMode.Create);
                    await prod.imageuploade.CopyToAsync(fs);
                    fs.Close();
                   
                }
                prod.image = imageName;
                db.Add(prod);
                await db.SaveChangesAsync();

                TempData["success"] = "the prooduct has been add";

                return RedirectToAction("Index");
            }



            return View(prod);
        }

        public async Task<IActionResult> Details(int id)
        {
            product pro = await db.products.Include(x=>x.category).FirstOrDefaultAsync(i=>i.id==id);

            if (pro == null)
            {
                return NotFound();
            }

            return View(pro);
        }

        public async Task<IActionResult> Edit(int id)
        {
            product prod = await db.products.FindAsync(id);

            if (prod == null)
            {
                return NotFound();
            }
            ViewBag.category = new SelectList(db.categories.OrderBy(c => c.sorting), "id", "name");
            return View(prod);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,product prod)
        {
            ViewBag.category = new SelectList(db.categories.OrderBy(c => c.sorting), "id", "name",prod.categoryid);

            if (ModelState.IsValid)
            {
                prod.slug = prod.name.ToLower().Replace(" ", "-");

                var slug = await db.products.Where(z=>z.id !=id).FirstOrDefaultAsync(x => x.slug == prod.slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "product is exostes");
                    return View(prod);
                }

                 

                if (prod.imageuploade != null)
                {
                    string uploaddir = Path.Combine(webHostEnvironment.WebRootPath, "madia/product");
                    if(! string.Equals(prod.image, "cc.jpg"))
                    {
                        string oldpath = Path.Combine(uploaddir, prod.image);
                        if (System.IO.File.Exists(oldpath))
                        {
                            System.IO.File.Delete(oldpath);
                        }
                    }

                    string imageName = Guid.NewGuid().ToString() + "_" + prod.imageuploade.FileName;
                    string filepath = Path.Combine(uploaddir, imageName);
                    FileStream fs = new FileStream(filepath, FileMode.Create);
                    await prod.imageuploade.CopyToAsync(fs);
                    fs.Close();
                    prod.image = imageName;
                }
                
                db.Update(prod);
                await db.SaveChangesAsync();

                TempData["success"] = "the prooduct has been updated";

                return RedirectToAction("Index");
            }



            return View(prod);
        }

        public async Task<IActionResult> Delete(int id)
        {
            product prod = await db.products.FindAsync(id);

            db.page.RemoveRange();

            if (prod == null)
            {
                TempData["Error"] = "the product dosent existe";
            }
            else
            {


                if (!string.Equals(prod.image, "cc.jpg"))
                {
                    string uploaddir = Path.Combine(webHostEnvironment.WebRootPath, "madia/product");
                    string oldpath = Path.Combine(uploaddir, prod.image);
                    if (System.IO.File.Exists(oldpath))
                    {
                        System.IO.File.Delete(oldpath);
                    }
                }

                db.products.Remove(prod);
                await db.SaveChangesAsync();
                TempData["success"] = "the product has been removed";
            }
            return RedirectToAction("Index");
        }


    }
}
