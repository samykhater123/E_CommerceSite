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
    }
}
