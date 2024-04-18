namespace TinyUrl.FunctionalServices;

public class TinyUrlFunctionalService : ITinyUrlProvider
{
    private readonly ITinyUrlDataProvider _tinyUrlDataProvider;
    private readonly IHashProvider _hashProvider;
    private readonly ILogger<TinyUrlFunctionalService> _logger;

    public TinyUrlFunctionalService(
               ITinyUrlDataProvider tinyUrlDataProvider,
               IHashProvider hashProvider,
               ILogger<TinyUrlFunctionalService> logger)
    {
        _tinyUrlDataProvider = tinyUrlDataProvider;
        _hashProvider = hashProvider;
        _logger = logger;
        _tinyUrlDataProvider = tinyUrlDataProvider;
        _hashProvider = hashProvider;
    }

    public string CreateTinyUrl(string longUrl, int? length = 7, string? shortUrl = null)
    {
        if (string.IsNullOrEmpty(shortUrl))
        {
            // Assert that record does not exist
            // If found, return the existing short url
            var existingShortUrls = _tinyUrlDataProvider.GetShortUrl(longUrl);
            if (existingShortUrls.Any())
            {
				// TODO: 
                return existingShortUrls.First();
            }

            string hashValue = _hashProvider.ComputeHashValue(longUrl);
            if (length != null)
            {
                hashValue = hashValue.Substring(0, length.Value);
            }
            _tinyUrlDataProvider.SaveUrlMapping(longUrl, hashValue);
            _tinyUrlDataProvider.SaveChanges();
            return hashValue;
        }

        _tinyUrlDataProvider.SaveUrlMapping(longUrl, shortUrl);
		_tinyUrlDataProvider.SaveChanges();
        return shortUrl;
    }

    public void DeleteAssociatedShortUrl(string longUrl)
    {
        _logger.LogInformation("Deleting short url for long url: {longUrl}", longUrl);
        var shortUrl = _tinyUrlDataProvider.GetShortUrl(longUrl);
        _logger.LogInformation("Short url found: {shortUrl}", shortUrl);
        _tinyUrlDataProvider.Delete(longUrl);
		_tinyUrlDataProvider.SaveChanges();
        _logger.LogInformation("Short url deleted: {shortUrl}", shortUrl);
    }
}
