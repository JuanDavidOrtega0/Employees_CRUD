using Microsoft.EntityFrameworkCore;
using Solucion.Models;

namespace Solucion.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}