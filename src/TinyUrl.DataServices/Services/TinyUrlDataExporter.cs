namespace TinyUrl.DataServices.Services;

public class TinyUrlDataExporter : ITinyUrlDataExporter
{
    private readonly ITinyUrlDataProvider _dataProvider;

    public TinyUrlDataExporter(ITinyUrlDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public void ExportUrlMappings(string filePath)
    {
        _dataProvider.GetUrlMappings().ToList().ForEach(x => File.AppendAllText(filePath, $"{x.Key} {x.Value}\n"));
    }
}

