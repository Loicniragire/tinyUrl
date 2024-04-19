namespace TinyUrl.DataServices.Services;

public class TinyUrlLinkedListDataService : ITinyUrlDataProvider
{
    private const string UrlHashTableFileName = "urlHashTable.json";
    private readonly string _tinyUrlDataFilePath;
    private readonly UrlHashTable _urlHashTable;

    public TinyUrlLinkedListDataService(int size)
    {
        _urlHashTable = new UrlHashTable(size);
        _tinyUrlDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UrlHashTableFileName);
    }

    public TinyUrlLinkedListDataService()
    {
        _tinyUrlDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UrlHashTableFileName);
        _urlHashTable = new UrlHashTable(_tinyUrlDataFilePath);
    }

    public void Delete(string shortUrl)
    {
        _urlHashTable.Delete(shortUrl);
    }

    public string GetLongUrl(string shortUrl)
    {
        return _urlHashTable.SearchByShortUrl(shortUrl);
    }

    public List<string> GetShortUrl(string longUrl)
    {
        return _urlHashTable.SearchByLongUrl(longUrl);
    }

    // takes a ShortUrl instead....
    public int GetLongUrlAccessCount(string longUrl)
    {
        return _urlHashTable.GetAccessCount(longUrl);
    }

    public void SaveChanges()
    {
        var jsonString = JsonSerializer.Serialize(_urlHashTable);
        File.WriteAllText(_tinyUrlDataFilePath, jsonString);
    }


    public void SaveUrlMapping(string longUrl, string shortUrl)
    {
        _urlHashTable.Insert(shortUrl, longUrl);
    }
}

