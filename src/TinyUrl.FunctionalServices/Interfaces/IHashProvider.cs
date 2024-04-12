namespace TinyUrl.FunctionalServices.Interfaces;

public interface IHashProvider
{
	string ComputeHashValue(string input);
}

