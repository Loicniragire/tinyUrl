namespace TinyUrl.DataServices.Interfaces;

public interface ITinyUrlDataExporter
{
    void ExportUrlMappings(string filePath);
}
