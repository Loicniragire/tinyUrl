namespace TinyUrl.Tests;

[TestFixture]
public class HashProviderTests
{
	[Test]
	public void ShouldGenerateHashValueGivenAString()
	{
		//Arrange
		var hashProvider = new HashingFunctionalService();
		var input = "https://www.google.com";

		//Act
		var hashValue = hashProvider.ComputeHashValue(input);

		//Assert
		Assert.That(hashValue, Is.Not.Null);
	}
}

