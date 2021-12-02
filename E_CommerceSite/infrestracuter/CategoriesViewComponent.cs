using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.infrestracuter
{
    public class CategoriesViewComponent: ViewComponent
    {
       
            private readonly Ecommersitecontext db;


            public CategoriesViewComponent(Ecommersitecontext db)
            {
                this.db = db;

            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var categoris = await GetcategorisAsync();
                return View(categoris);
            }

            private Task<List<Category>> GetcategorisAsync()
            {

                return db.categories.OrderBy(x => x.sorting).ToListAsync();
            }
        }
    }

