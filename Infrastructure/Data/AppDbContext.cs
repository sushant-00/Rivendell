using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using Domain.Entites;

namespace Infrastructure.Data
{
    public  class AppDbContext(DbContextOptions<AppDbContext> options ):DbContext(options)
    {
        public DbSet<AppUsers> Users { get; set; }
    }
}
