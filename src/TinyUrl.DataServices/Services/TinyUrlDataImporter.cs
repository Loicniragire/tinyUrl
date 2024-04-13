
namespace TinyUrl.DataServices.Services;

public class TinyUrlDataImporter : ITinyUrlDataImporter
{
	private ConcurrentDictionary<string, string> _tinyUrlMap = new ConcurrentDictionary<string, string>();

    public ConcurrentDictionary<string, string> TinyUrlMap => _tinyUrlMap;

    public void Import(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found at {filePath}");
        }

        // Open the file and read the data line by line
        File.ReadAllLines(filePath).ToList().ForEach(line =>
        {
            // Split the line by space
            // The first part is the longUrl and the seconf part is the list of tinyUrls
            // Insert the longUrl and tinyUrl into the TinyUrlMap
            var parts = line.Split(' ');
            var longUrl = parts[0];
            var tinyUrls = parts[1].Split(',');
            // concatinate the tinyUrls with comma into a string
            var tinyUrlsValue = string.Join(",", tinyUrls);
            //
            TinyUrlMap.TryAdd(longUrl, tinyUrlsValue);


        });
    }
}

