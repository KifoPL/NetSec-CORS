namespace Simple_API;

public static class Helpers
{
	private static readonly Random random = new();
	public static string Alphabet => "abcdefghijklmnopqrstuvwxyz";

	public static string GenerateName(int length)
	{
		string name = $"{Alphabet[random.Next(0, Alphabet.Length)]}";
		for (int i = 1; i < length; i++)
		{
			name += Alphabet[random.Next(0, Alphabet.Length)];
		}

		return name;
	}
}