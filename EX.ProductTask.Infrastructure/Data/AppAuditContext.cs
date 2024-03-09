using Core.Entities.Management;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;
public partial class AppHelperContext : DbContext
{
    public AppHelperContext(DbContextOptions<AppHelperContext> options
) : base(options)
    { }
    public DbSet<Audit> Audits { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //  modelBuilder.Entity<TagSearch>(e => e.HasQueryFilter(a => a.IsDeleted == false));
    }
}

