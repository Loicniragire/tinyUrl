namespace TinyUrl.FunctionalServices.Interfaces;

public interface ITinyUrlStatsProvider
{
	/// <summary>
	/// Returns the number of times a short URL has been clicked.
	/// i.e the number of times its longUrl has been retrieved.
	/// </summary>
	/// <param name="shortUrl">The short URL to get the number of times clicked for.</param>
	/// <returns>The number of times the short URL has been clicked.</returns>
	int GetNumberOfTimesClicked(string shortUrl);
}


