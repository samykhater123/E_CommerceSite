using E_CommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.infrestracuter
{
    public class MainmenuViewComponent:ViewComponent
    {
        private readonly Ecommersitecontext db;


        public MainmenuViewComponent(Ecommersitecontext db)
        {
            this.db = db;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var page = await GetpagesAsync();
            return View(page);
        }

        private Task <List<pages>> GetpagesAsync()
        {
           
            return db.page.OrderBy(x => x.sorting).ToListAsync();
        }
    }
}
