namespace Sample_App_Consuming_API;

public class Program
{
	private static readonly HttpClient HttpClient = new();

	public static async Task Main(string[] args)
	{

		string json = await HttpClient.GetStringAsync("https://localhost:7042/page/3");

		Console.WriteLine(json);
	}
}