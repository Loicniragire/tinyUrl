namespace TinyUrl.FunctionalServices.Interfaces;

public interface ITinyUrlProvider
{
	/// <summary>
	/// Creates a tiny URL to associate with a long URL.
	/// </summary>
	/// <param name="longUrl">The long URL to create a tiny URL for.</param>
	/// <param name="length">The length of the short URL to generate. Default is 7. Set to null to unrestrict the length.</param>
	/// <param name="shortUrl">The short URL to use. If null, a new short URL should be generated.</param>
	/// <returns>The tiny URL created for the long URL.</returns>
	string CreateTinyUrl(string longUrl, int? length = 7, string? shortUrl = null);

	/// <summary>
	/// Deletes the associated shortUrl for a given longUrl.
	/// </summary>
	/// <param name="longUrl">The longUrl whose corresponding shortUrl to delete.</param>
	/// <remarks>
	/// This method should delete the tiny URL and any associated statistics.
	/// </remarks>
	void DeleteAssociatedShortUrl(string longUrl);
}


