using Microsoft.EntityFrameworkCore;
using WebService.Model;
using System.Collections.Generic;

namespace WebService.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Car> CarTable { get; set; }
        public DbSet<User> UsersData{get; set;}
    }
}
