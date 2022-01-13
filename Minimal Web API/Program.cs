using Microsoft.EntityFrameworkCore;
using Minimal_Web_API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
	var forecast = Enumerable.Range(1, 5).Select(index =>
	   new WeatherForecast
	   (
		   DateTime.Now.AddDays(index),
		   Random.Shared.Next(-20, 55),
		   summaries[Random.Shared.Next(summaries.Length)]
	   ))
		.ToArray();
	return forecast;
})
.WithName("GetWeatherForecast");

Random random = new();
string letters = "abcdefghijklmnopqrstyuwxyz";

string GetName(int count)
{
	string name = "";
	for (int i = 0; i < count; i++)
	{
		name += letters[random.Next(0, letters.Length)];
	}

	return name;
}

app.MapPost("/seedDB/{count:int}",
	async (ApiDbContext context, int count) =>
	{
		for (int i = 0; i < count; i++)
		{
			string name = GetName(random.Next(4, 10));
			SomeData data = new() { Name = name };
			await context.DataTable.AddAsync(data);
		}

		await context.SaveChangesAsync();
	});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}