using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Simple_API.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
	   .AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.WriteIndented = true;
		});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader()
			  .AllowAnyMethod()
			  .WithOrigins("https://localhost:7529");
	});
	options.AddPolicy("read-only",
		policy =>
		{
			policy.AllowAnyOrigin()
				  .WithMethods("GET");
		});
});

builder.Services.AddDbContext<ApiContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
