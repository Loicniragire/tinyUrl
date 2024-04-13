namespace TinyUrl.Tests;

[TestFixture]
public class TinyUrlDataImporterTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ShouldImportTinyUrlMappingDataIntoMemory()
    {
        // Arrange
        var filePath = "TinyUrl_mapping_data.txt";
        var importer = new TinyUrlDataImporter();

        // Act
        importer.Import(filePath);
        var importedData = importer.TinyUrlMap;

        // Assert
        // Assert that the importedData is not null
        Assert.That(importedData, Is.Not.Null);

        // Assert that importedData contains two records
        Assert.That(importedData.Count, Is.EqualTo(2));

        // Assert that the first record has the longUrl "https://www.google.com"
        Assert.That(importedData.ContainsKey("http://www.google.com"), Is.True);

        // Assert that the second record has the longUrl "http://www.nba.com"
        Assert.That(importedData.ContainsKey("http://www.nba.com"), Is.True);
    }

    [Test]
    public void ShouldThrowArgumentNullExceptionWhenFilePathIsNull()
    {
        // Arrange
        var importer = new TinyUrlDataImporter();

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => importer.Import(""));
    }

    [Test]
    public void ShouldThrowsFileNotFoundExceptionWhenFileDoesNotExist()
    {
        // Arrange
        var filePath = "Missing_file.txt";
        var importer = new TinyUrlDataImporter();

        // Act
        // Assert
        Assert.Throws<FileNotFoundException>(() => importer.Import(filePath));
    }

}

