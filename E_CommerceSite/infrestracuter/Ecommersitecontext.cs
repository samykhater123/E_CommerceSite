using E_CommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceSite.infrestracuter
{
    public class Ecommersitecontext:DbContext
    {
        public Ecommersitecontext(DbContextOptions<Ecommersitecontext>options)
            :base(options)
        {

        }

        public DbSet<pages> page { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<product> products { get; set; }
    }
}
