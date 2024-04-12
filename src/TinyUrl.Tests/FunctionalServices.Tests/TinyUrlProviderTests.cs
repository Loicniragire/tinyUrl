namespace TinyUrl.Tests;

[TestFixture]
public class TinyUrlProviderTests
{
    private readonly Mock<ITinyUrlDataProvider> _tinyUrlDataProviderMock = new();
    private readonly Mock<IHashProvider> _hashProviderMock = new();
    private readonly Mock<ILogger<TinyUrlFunctionalService>> _loggerMock = new();

    [SetUp]
    public void SetUp()
    {
		// clear the invocations before each test
		_tinyUrlDataProviderMock.Invocations.Clear();

        _hashProviderMock.Setup(x => x.ComputeHashValue("https://www.google.com")).Returns("1234");
        _tinyUrlDataProviderMock.Setup(x => x.SaveUrlMapping("https://www.google.com", "1234"));
		_tinyUrlDataProviderMock.Setup(x => x.GetShortUrl("https://www.google.com")).Returns("tinyurl.com/1234");
		_tinyUrlDataProviderMock.Setup(x => x.Delete("https://www.google.com"));

    }

    [Test]
    public void ShouldAssignShortUrlToLongUrlWhenShortUrlIsProvided()
    {
        // Arrange
        var tinyUrlProvider = new TinyUrlFunctionalService(_tinyUrlDataProviderMock.Object,
                   _hashProviderMock.Object,
                   _loggerMock.Object);

        var longUrl = "https://www.google.com";
        var shortUrl = "tinyurl.com/1234";

        // Act
        var actualShortUrl = tinyUrlProvider.CreateTinyUrl(longUrl, null, shortUrl);

        // Assert
		// Assert that ComputeHashValue was never called
		_hashProviderMock.Verify(x => x.ComputeHashValue(It.IsAny<string>()), Times.Never);

		// Assert that SaveUrlMapping was called once
		_tinyUrlDataProviderMock.Verify(x => x.SaveUrlMapping(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

		// Assert that the actual short url is equal to the expected short url
        Assert.That(actualShortUrl, Is.EqualTo(shortUrl));
    }

	[Test]
	public void ShouldIgnoreLengthWhenShortUrlIsProvided()
	{
		// Arrange
		var tinyUrlProvider = new TinyUrlFunctionalService(_tinyUrlDataProviderMock.Object,
				   _hashProviderMock.Object,
				   _loggerMock.Object);
		var longUrl = "https://www.google.com";
		var shortUrl = "tinyurl.com/1234";

		// Act
		var actualShortUrl = tinyUrlProvider.CreateTinyUrl(longUrl, 5, shortUrl);

		// Assert
		// Assert that ComputeHashValue was never called
		_hashProviderMock.Verify(x => x.ComputeHashValue(It.IsAny<string>()), Times.Never);

		// Assert that SaveUrlMapping was called once
		_tinyUrlDataProviderMock.Verify(x => x.SaveUrlMapping(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

		// Assert that the actual short url is equal to the expected short url
		Assert.That(actualShortUrl, Is.EqualTo(shortUrl));

	}

	[Test]
	public void ShouldNotCreateNewTinyUrlEntryWhenRecordExistsAndUserIsNotProvidingCustomShortUrl()
	{
		// Arrange
		var tinyUrlProvider = new TinyUrlFunctionalService(_tinyUrlDataProviderMock.Object,
				   _hashProviderMock.Object,
				   _loggerMock.Object);
		var longUrl = "https://www.google.com";
		// mock tinyDataProvider to return null the first time and a short url the second time
		_tinyUrlDataProviderMock.SetupSequence(x => x.GetShortUrl(It.IsAny<string>()))
			.Returns("")
			.Returns("tinyurl.com/1234");

		// Act
		var actualShortUrl = tinyUrlProvider.CreateTinyUrl(longUrl);

		// Attempt to create a new tiny url entry with the same long url
		var actualShortUrl2 = tinyUrlProvider.CreateTinyUrl(longUrl);

		// Assert
		// ComputeHashValue should be called once
		_hashProviderMock.Verify(x => x.ComputeHashValue(It.IsAny<string>()), Times.Once);

		// SaveUrlMapping should be called once
		_tinyUrlDataProviderMock.Verify(x => x.SaveUrlMapping(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
	}

	[Test]
	public void ShouldComputeHashValueWhenShortUrlIsNotProvided()
	{
		// Arrange
        var tinyUrlProvider = new TinyUrlFunctionalService(_tinyUrlDataProviderMock.Object,
                   _hashProviderMock.Object,
                   _loggerMock.Object);
        var longUrl = "https://www.google.com";

		// Act
        var actualShortUrl = tinyUrlProvider.CreateTinyUrl(longUrl);

		// Assert
		// Assert that ComputeHashValue was called once
		_hashProviderMock.Verify(x => x.ComputeHashValue(It.IsAny<string>()), Times.Once);

		//
		// Assert that SaveUrlMapping was called once
		_tinyUrlDataProviderMock.Verify(x => x.SaveUrlMapping(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
	}

	[Test]
	public void ShouldRetrieveCorrespondingShortUrlWhenDeleting()
	{
		// Arrange
		var tinyUrlProvider = new TinyUrlFunctionalService(_tinyUrlDataProviderMock.Object,
				   _hashProviderMock.Object,
				   _loggerMock.Object);
		var longUrl = "https://www.google.com";

		// Act
		tinyUrlProvider.DeleteAssociatedShortUrl(longUrl);

		// Assert
		// Assert that GetShortUrl was called once
		_tinyUrlDataProviderMock.Verify(x => x.GetShortUrl(It.IsAny<string>()), Times.Once);

		// Assert that DeleteShortUrl was called once
		_tinyUrlDataProviderMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
	}

}

