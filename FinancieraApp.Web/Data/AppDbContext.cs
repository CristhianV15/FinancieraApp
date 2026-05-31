using FinancieraApp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancieraApp.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Banco> Bancos => Set<Banco>();
}