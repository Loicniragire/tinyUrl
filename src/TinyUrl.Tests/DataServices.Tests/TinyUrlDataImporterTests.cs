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
        Assert.IsNotNull(importedData);
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

