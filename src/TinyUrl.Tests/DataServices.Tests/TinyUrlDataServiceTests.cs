namespace TinyUrl.Tests;

[TestFixture]
public class TinyUrlDataServiceTests
{
    private readonly Mock<ITinyUrlDataImporter> _mockDataImporter = new();

    [Test]
    public void ShouldInitializeUrlMappingFromFileWhenInstatiated()
    {
        // Arrange
        // Initialize a ConcurrentDictionary<string, string> with 5 random key-value pairs
        var tinyUrlMap = new ConcurrentDictionary<string, string>();
        tinyUrlMap.TryAdd("https://www.google.com", "1234567");
        tinyUrlMap.TryAdd("https://www.abc.com", "0987654,abc");

        _mockDataImporter.Setup(x => x.Import(It.IsAny<string>()));
        _mockDataImporter.Setup(x => x.TinyUrlMap).Returns(tinyUrlMap);

        // Act
        var tinyUrlDataService = new TinyUrlDataService(_mockDataImporter.Object, "dummyFilePath");

        // Assert
        // Assert that tinyUrlDataImporter.Import() is called once
        _mockDataImporter.Verify(x => x.Import(It.IsAny<string>()), Times.Once);

    }

}

