using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Minimal_Web_API.Data;

public class SomeData
{
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime Timestamp { get; set; }
}

public class SomeDataConfiguration : IEntityTypeConfiguration<SomeData>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<SomeData> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Name).IsRequired().HasMaxLength(200).IsUnicode(false);
		builder.Property(x => x.Timestamp).HasDefaultValueSql("getdate()");
	}
}