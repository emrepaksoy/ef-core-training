using AutoMapperDemo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperDemo.API.Context
{
    public class AutoMapDbContext : DbContext
    {

        public AutoMapDbContext(DbContextOptions options) : base(options)
        {
        }
        public AutoMapDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
