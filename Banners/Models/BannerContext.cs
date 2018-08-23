using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Banners.Models
{
    public class BannerContext: DbContext 
    {
        public BannerContext(DbContextOptions<BannerContext> options): base(options)
        {
            
        }

        public DbSet<Banner> Banners { get; set; }
    }
}
