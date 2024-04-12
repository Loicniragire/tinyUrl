namespace TinyUrl.FunctionalServices;

public class TinyUrlStatsFunctionalService: ITinyUrlStatsProvider
{
    private readonly ITinyUrlDataProvider _tinyUrlDataProvider;
	private readonly ILogger<TinyUrlStatsFunctionalService> _logger;


	public TinyUrlStatsFunctionalService(ITinyUrlDataProvider tinyUrlDataProvider, ILogger<TinyUrlStatsFunctionalService> logger)
	{
		_tinyUrlDataProvider = tinyUrlDataProvider;
		_logger = logger;
	}

	public int GetNumberOfTimesClicked(string longUrl)
	{
		var accessCount = _tinyUrlDataProvider.GetLongUrlAccessCount(longUrl);
		_logger.LogInformation($"Number of times longUrl has been accessed: {accessCount}");
		return accessCount;
	}
}


