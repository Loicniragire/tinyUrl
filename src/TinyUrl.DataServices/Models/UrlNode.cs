namespace TinyUrl.DataServices.Models;

[Serializable]
public class UrlNode
{
    public string TinyUrl { get; set; }
    public string LongUrl { get; set; }
    public int AccessCount { get; set; }
    public UrlNode Next { get; set; }

    public UrlNode(string tinyUrl, string longUrl)
    {
        TinyUrl = tinyUrl;
        LongUrl = longUrl;
        AccessCount = 0;
        Next = null;
    }
}

