namespace TinyUrl.FunctionalServices;

public class HashingFunctionalService : IHashProvider
{
	public string ComputeHashValue(string input)
	{
		// Compute the hash value of the input
		// Use MD5 algorithm to compute the hash value
		// Convert the byte array to a string
		// Return the hash value
		using (MD5 md5 = MD5.Create())
		{
			byte[] inputBytes = Encoding.ASCII.GetBytes(input);
			byte[] hashBytes = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("X2"));
			}

			return sb.ToString();
		}
	}
}
