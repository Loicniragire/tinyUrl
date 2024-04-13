namespace TinyUrl.DataServices.Interfaces;

public interface ITinyUrlDataImporter
{
    ConcurrentDictionary<string, string> TinyUrlMap { get; }
    void Import(string filePath);
}

