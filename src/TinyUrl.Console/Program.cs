/// <summary>
/// This is a simple console application that demonstrates the functionality of the TinyUrl service.
/// Supported operations include:
/// 1. Creating and Deleting short URLs with associated long URLs
/// 2. Retrieving the long URL associated with a short URL
/// 3. Retrieving the number of times a short URL has been clicked
/// 4. Entering a custom short URL 
/// 5. Exporting the URL mappings to a file
/// </summary>

var tinyUrlDataImporter = new TinyUrlDataImporter();

var tinyUrlDataProvider = new TinyUrlLinkedListDataService();
var hashProvider = new HashingFunctionalService();
var functionalServiceLogger = new Logger<TinyUrlFunctionalService>(new LoggerFactory());
var tinyUrlFunctionalService = new TinyUrlFunctionalService(tinyUrlDataProvider, hashProvider, functionalServiceLogger);
var tinyUrlStatsLogger = new Logger<TinyUrlStatsFunctionalService>(new LoggerFactory());
var tinyUrlRetrieverLogger = new Logger<TinyUrlRetrieverFunctionalService>(new LoggerFactory());
var tinyUrlStatsService = new TinyUrlStatsFunctionalService(tinyUrlDataProvider, tinyUrlStatsLogger);
var tinyUrlRetrieverFunctionalService = new TinyUrlRetrieverFunctionalService(tinyUrlDataProvider, tinyUrlRetrieverLogger);

var tinyUrlBase = "http://tinyurl.com/";


// import a json file containing a list of ProcessRequest objects
var json = File.ReadAllText("InputCommands.json");
var processRequests = JsonSerializer.Deserialize<List<ProcessRequest>>(json);

// Loop through each ProcessRequest object and perform the requested action
foreach (var request in processRequests)
{
    if (request.Action == "Create")
    {
        CreateTinyUrl(request);
    }
}

void CreateTinyUrl(ProcessRequest request)
{
    // Generate a short URL
    if (request.ShortUrl == null)
    {
        var shortUrl = tinyUrlFunctionalService.CreateTinyUrl(request.LongUrl);
    }
    else
    {
        var shortUrl = tinyUrlFunctionalService.CreateTinyUrl(longUrl: request.LongUrl, shortUrl: request.ShortUrl);
    }
}
