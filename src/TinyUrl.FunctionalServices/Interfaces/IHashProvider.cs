namespace TinyUrl.FunctionalServices.Interfaces;

public interface IHashProvider
{
	/// <summary>
	/// A hash function that hashes a longUrl.
	/// </summary>
    string ComputeHashValue(string input);
}
