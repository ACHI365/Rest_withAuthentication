using Microsoft.EntityFrameworkCore;
using Registration.Modules;

namespace Registration.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options){}

    public DbSet<Blogs> Blogs { get; set; }
    public DbSet<User> Users { get; set; }


}