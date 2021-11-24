using E_CommerceSite.infrestracuter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.Models
{
    public class seeddata
    {
        public static void initialize(IServiceProvider serviceprovider)
        {
            using (var context = new Ecommersitecontext(serviceprovider.GetRequiredService<DbContextOptions<Ecommersitecontext>>()))
            {
                if (context.page.Any())
                {
                    return;
                }
                context.page.AddRange(
                    new pages
                    {
                        titel = "Home",
                        slug = "home",
                        contant = "home page",
                        sorting = 0
                    },
                    new pages
                    {
                        titel = "About us",
                        slug = "about-us",
                        contant = "about us page",
                        sorting = 100
                    },
                    new pages
                    {
                        titel = "Services",
                        slug = "services",
                        contant = "services page",
                        sorting = 100
                    },
                    new pages
                    {
                        titel = "Contact",
                        slug = "contact",
                        contant = "contact page",
                        sorting = 100
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
