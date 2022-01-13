HttpClient httpClient = new();

var data = async () =>
{
	string result;
	try { result = await httpClient.GetStringAsync("https://localhost:7042/page/3"); }
	catch (HttpRequestException e) { result = $"{e.StatusCode?.ToString() ?? "No Code"} error -\n{e.Message}"; }
	catch (Exception e) { result = $"{e.GetType().Name} error -\n{e.Message}"; }

	return result;
};

string json = await data.Invoke();

Console.WriteLine(json);