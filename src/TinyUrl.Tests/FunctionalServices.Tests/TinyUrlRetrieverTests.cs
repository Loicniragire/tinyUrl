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
        _tinyUrlDataProviderMock.Setup(x => x.IncrementLongUrlAccessCount("https://www.abc.com"));
    }

    [Test]
    public void ShouldIncrementLongUrlAccessCountWhenRetrievingLongUrl()
    {
        // Arrange
        var shortUrl = "tinyurl.com/abc";
        var longUrl = "https://www.abc.com";
        var tinyUrlRetriever = new TinyUrlRetrieverFunctionalService(_tinyUrlDataProviderMock.Object, _loggerMock.Object);

        // Act
        var actualLongUrl = tinyUrlRetriever.RetrieveLongUrl(shortUrl);

        // Assert
        Assert.That(actualLongUrl, Is.EqualTo(longUrl));

        // Verify that the IncrementLongUrlAccessCount method was called with the correct longUrl
        _tinyUrlDataProviderMock.Verify(x => x.IncrementLongUrlAccessCount(actualLongUrl), Times.Once);
    }

}

