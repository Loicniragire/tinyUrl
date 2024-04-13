namespace TinyUrl.FunctionalServices;

public class TinyUrlRetrieverFunctionalService : ITinyUrlRetriever
{
    private readonly ITinyUrlDataProvider _tinyUrlDataProvider;
    private readonly ILogger<TinyUrlRetrieverFunctionalService> _logger;

    public TinyUrlRetrieverFunctionalService(ITinyUrlDataProvider tinyUrlDataProvider, ILogger<TinyUrlRetrieverFunctionalService> logger)
    {
        _tinyUrlDataProvider = tinyUrlDataProvider;
        _logger = logger;
    }

    public string RetrieveLongUrl(string shortUrl)
    {
        _logger.LogInformation("Retrieving long url for short url: {shortUrl}", shortUrl);
        var longUrl = _tinyUrlDataProvider.GetLongUrl(shortUrl);
        _logger.LogInformation("Long url retrieved: {longUrl}", longUrl);

        // increment the number of times the longUrl has been accessed
        _tinyUrlDataProvider.IncrementLongUrlAccessCount(longUrl);
        return longUrl;
    }
}


