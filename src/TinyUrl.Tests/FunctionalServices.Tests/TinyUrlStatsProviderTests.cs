namespace TinyUrl.Tests;

[TestFixture]
public class TinyUrlStatsProviderTests
{
    private readonly Mock<ITinyUrlDataProvider> _tinyUrlDataProviderMock = new();
    private readonly Mock<ITinyUrlRetriever> _tinyUrlRetrieverMock = new();
    private readonly Mock<ILogger<TinyUrlStatsFunctionalService>> _loggerMock = new();

    [SetUp]
    public void Setup()
    {
        _tinyUrlDataProviderMock.Reset();
        _tinyUrlDataProviderMock.Setup(x => x.GetLongUrl("tinyurl.com/abc")).Returns("https://www.abc.com");
        _tinyUrlDataProviderMock.Setup(x => x.GetLongUrlAccessCount("http://www.google.com")).Returns(1);

        _tinyUrlRetrieverMock.Setup(x => x.RetrieveLongUrl("tinyurl.com/abc")).Returns("https://www.google.com");
    }

    [Test]
    public void ShouldReturnTheNumberOfTimesALongUrlHasBeenAccessed()
    {
        // Arrange
        var tinyUrlStatsProvider = new TinyUrlStatsFunctionalService(_tinyUrlDataProviderMock.Object, _loggerMock.Object);
        var longUrl = "http://www.google.com";
        var shortUrl = "tinyurl.com/abc";

        // Act
        Mock<ILogger<TinyUrlRetrieverFunctionalService>> _retrieverLoggerMock = new();
        var tinyUrlRetriever = new TinyUrlRetrieverFunctionalService(_tinyUrlDataProviderMock.Object, _retrieverLoggerMock.Object);
        var actualLongUrl = tinyUrlRetriever.RetrieveLongUrl(shortUrl);

        var numberClicked = tinyUrlStatsProvider.GetNumberOfTimesClicked(longUrl);

        // Assert
        Assert.That(numberClicked, Is.EqualTo(1));

        // Assert that GetLongUrlAccessCount was called once
        _tinyUrlDataProviderMock.Verify(x => x.GetLongUrlAccessCount(longUrl), Times.Once);


    }
}

