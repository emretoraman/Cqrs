using Cqrs.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cqrs.Context
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync();
    }
}