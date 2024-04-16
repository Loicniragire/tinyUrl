namespace TinyUrl.DataServices.Interfaces;

public interface ITinyUrlDataProvider
{
    string GetLongUrl(string shortUrl);
    string GetShortUrl(string longUrl);
    void Delete(string shortUrl);
    void SaveUrlMapping(string longUrl, string shortUrl);
    void IncrementLongUrlAccessCount(string longUrl);
    int GetLongUrlAccessCount(string longUrl);
    void SaveChanges();
}


