using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CohortsFirstHW.Models;

namespace CohortsFirstHW.Data
{
    public class CohortsFirstHWContext : DbContext
    {
        public CohortsFirstHWContext (DbContextOptions<CohortsFirstHWContext> options)
            : base(options)
        {
        }

        public DbSet<CohortsFirstHW.Models.Product> Products { get; set; } = default!;
    }
}
