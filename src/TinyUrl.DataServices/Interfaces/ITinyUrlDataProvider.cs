namespace TinyUrl.DataServices.Interfaces;

public interface ITinyUrlDataProvider
{
    string GetLongUrl(string shortUrl);
    List<string> GetShortUrl(string longUrl);
    void Delete(string shortUrl);
    void SaveUrlMapping(string longUrl, string shortUrl);
    int GetLongUrlAccessCount(string longUrl);
    void SaveChanges();
}


