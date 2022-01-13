using Microsoft.EntityFrameworkCore;

namespace Minimal_Web_API.Data;

public class ApiDbContext : DbContext
{
	/// <inheritdoc />
	protected ApiDbContext() {
	}

	/// <inheritdoc />
	public ApiDbContext(DbContextOptions options) : base(options)
	{
	}

	public virtual DbSet<SomeData> DataTable { get; set; }

	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new SomeDataConfiguration());
	}
}