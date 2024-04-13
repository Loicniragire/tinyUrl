namespace TinyUrl.DataServices.Services;

public class TinyUrlDataService : ITinyUrlDataProvider
{
	private readonly string _tinyUrlDataFilePath;
	private readonly ITinyUrlDataImporter _tinyUrlDataImporter;

    // mapping of short URL to long URL
    // key: long URL
    // value: short URL
    private ConcurrentDictionary<string, string> _urlMapping;

    // create a dictionary to store the short URL and the access count
    // key: short URL
    // value: access count
    private ConcurrentDictionary<string, int> _longUrlAccessCount = new ConcurrentDictionary<string, int>();

	public TinyUrlDataService(ITinyUrlDataImporter tinyUrlDataImporter, string tinyUrlDataFilePath)
	{
		_tinyUrlDataImporter = tinyUrlDataImporter;
		_tinyUrlDataFilePath = tinyUrlDataFilePath;
		_tinyUrlDataImporter.Import(_tinyUrlDataFilePath);
		_urlMapping = _tinyUrlDataImporter.TinyUrlMap;
	}

    public string GetLongUrl(string shortUrl)
    {
        return _urlMapping.FirstOrDefault(x => x.Value == shortUrl).Key;
    }

    public string GetShortUrl(string longUrl)
    {
        return _urlMapping.TryGetValue(longUrl, out string shortUrl) ? shortUrl : null;
    }

    public void SaveUrlMapping(string longUrl, string shortUrl)
    {
        _urlMapping.AddOrUpdate(longUrl, shortUrl, (key, oldValue) => $"{oldValue},{shortUrl}");
    }

    public void Delete(string longUrl)
    {
        // Remove the mapping of short URL to long URL from the dictionary
        _urlMapping.TryRemove(longUrl, out _);
    }

    public void IncrementLongUrlAccessCount(string longUrl)
    {
        _longUrlAccessCount.AddOrUpdate(longUrl, 1, (key, oldValue) => oldValue + 1);
    }

    public int GetLongUrlAccessCount(string longUrl)
    {
        return _longUrlAccessCount[longUrl];
    }

    public ConcurrentDictionary<string, string> GetUrlMappings()
    {
        return _urlMapping;
    }
}
