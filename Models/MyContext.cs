#pragma warning disable CS8618

namespace CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

public class MyContext : DbContext 
{     
    public DbSet<Dish> Dishes { get; set; } 

    public MyContext(DbContextOptions options) : base(options) { }    
}