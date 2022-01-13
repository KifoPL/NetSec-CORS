using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_API.Data;

namespace Simple_API.Controllers;

[Controller]
public class SampleDataController : ControllerBase
{
	private readonly ApiContext _context;
	private readonly Random _random;

	public SampleDataController(ApiContext context)
	{
		_context = context;
		_random = new();
	}

	[Route("/")]
	[HttpGet]
	public async Task<IActionResult> GetData() { return Ok(await _context.DataTable.ToListAsync()); }

	[Route("/page/{page:int}")]
	[HttpGet]
	public async Task<IActionResult> GetPagedData(int page = 1, int size = 10)
	{
		return Ok(await _context.DataTable.Skip((page - 1) * size).Take(size).ToListAsync());
	}

	[HttpPost]
	[Route("/seed/{count:int}")]
	public async Task<IActionResult> Seed(int count = 5)
	{
		List<SomeData> dataList = new();
		for (var i = 0; i < count; i++)
		{
			SomeData data = new() { Name = Helpers.GenerateName(_random.Next(4, 10)) };
			dataList.Add(data);
		}

		await _context.DataTable.AddRangeAsync(dataList);

		int addedCount = await _context.SaveChangesAsync();
		return StatusCode((int)HttpStatusCode.Created, new { AddedCount = addedCount, Data = dataList });
	}
}