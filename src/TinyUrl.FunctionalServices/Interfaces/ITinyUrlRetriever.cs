namespace TinyUrl.FunctionalServices.Interfaces;

public interface ITinyUrlRetriever
{
	/// <summary>
	/// Returns the long URL associated with a short URL.
	/// </summary>
	/// <param name="shortUrl">The short URL to retrieve the long URL for.</param>
	/// <returns>The long URL associated with the short URL.</returns>
	string RetrieveLongUrl(string shortUrl);
}

