﻿using Cqrs.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cqrs.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Product> Products { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
