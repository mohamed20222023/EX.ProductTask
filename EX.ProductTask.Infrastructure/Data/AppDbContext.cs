using System.Reflection;
using Core.Entities.Management;
using Core.Entities.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;
public partial class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options
    ) : base(options)
    { }
    #region  BasicData
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

	#endregion

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		//Seeding a  'Administrator' role to AspNetRoles table
		modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Admin", NormalizedName = "ADMIN".ToUpper() });
		modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7211", Name = "User", NormalizedName = "USER".ToUpper() });


		//a hasher to hash the password before seeding the user to the db
		var hasher = new PasswordHasher<IdentityUser>();


		//Seeding the User to AspNetUsers table
		modelBuilder.Entity<IdentityUser>().HasData(
			new IdentityUser
			{
				Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
				UserName = "Admin",
				NormalizedUserName = "ADMIN",
				Email = "Admin@admin.com",
				NormalizedEmail = "ADMIN@ADMIN.COM",
				PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
				SecurityStamp = Guid.NewGuid().ToString(),
				ConcurrencyStamp = Guid.NewGuid().ToString()
			}
		);


		//Seeding the relation between our user and role to AspNetUserRoles table
		modelBuilder.Entity<IdentityUserRole<string>>().HasData(
			new IdentityUserRole<string>
			{
				RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
				UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
			}
		);

		modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Electronics" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Clothing" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Home and Garden" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Sports and Outdoors" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Books" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 6, Name = "Toys and Games" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 7, Name = "Beauty and Health" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 8, Name = "Automotive" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 9, Name = "Grocery" });
		modelBuilder.Entity<Category>().HasData(new Category { Id = 10, Name = "Jewelry" });



	}
}

