using Microsoft.EntityFrameworkCore;

namespace Simple_API.Data;

public class ApiContext : DbContext
{
	/// <inheritdoc />
	protected ApiContext() {
	}

	/// <inheritdoc />
	public ApiContext(DbContextOptions options) : base(options)
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