using CompanyEmployees.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Infrastructure.Persistence
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Employee>? Employees { get; set; }
    }
}
