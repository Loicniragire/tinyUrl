namespace TinyUrl.Tests;

[TestFixture]
public class TinyUrlRetrieverTests
{
    private readonly Mock<ITinyUrlDataProvider> _tinyUrlDataProviderMock = new();
    private readonly Mock<ILogger<TinyUrlRetrieverFunctionalService>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _tinyUrlDataProviderMock.Reset();
        _tinyUrlDataProviderMock.Setup(x => x.GetLongUrl("tinyurl.com/abc")).Returns("https://www.abc.com");
    }

}

