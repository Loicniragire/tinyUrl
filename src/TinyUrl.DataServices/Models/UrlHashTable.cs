namespace TinyUrl.DataServices.Models;

/// <summary>
/// Implements an array-based Hash Table to store URL mappings.
/// It resolves collisions by chaining.
/// </summary>
[Serializable]
public class UrlHashTable
{
    private int _size;
    private UrlNode[] _buckets;

	public UrlNode[] Buckets {get => _buckets; set => _buckets = value;}
    public int Size {get => _size; set => _size = value;}

    public UrlHashTable(){}
	public UrlHashTable(string jsonFilename)
	{
		if (File.Exists(jsonFilename))
		{
			var json = File.ReadAllText(jsonFilename);
			var data = JsonSerializer.Deserialize<UrlHashTable>(json);
			_size = data.Buckets.Length;
			_buckets = data.Buckets;
		}
		else
		{
			_size = 1000;
			_buckets = new UrlNode[_size];
		}
	}

	public UrlHashTable(int size)
	{
		_size = size;
		_buckets = new UrlNode[size];
	}

    private int GetHash(string longUrl)
    {
        int hash = 0;
        foreach (char c in longUrl)
        {
            hash = (hash * 31 + c) % _size;
        }
        return hash;
    }

    public void Insert(string tinyUrl, string longUrl)
    {
        int index = GetHash(longUrl);
        UrlNode node = new UrlNode(tinyUrl, longUrl);

        if (_buckets[index] == null)
        {
            _buckets[index] = node;
        }
        else
        {
            UrlNode current = _buckets[index];
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = node;
        }
    }

	/// <summary>
	/// Searches for all short URLs associated with a given long URL.
	/// </summary>
	/// <param name="longUrl">The long URL to search for.</param>
	/// <returns>A list of short URLs associated with the long URL.</returns>
    public List<string> SearchByLongUrl(string longUrl)
    {
        var shortUrls = new List<string>();
        var index = GetHash(longUrl);
        var current = _buckets[index];
        while (current != null)
        {
            if (current.LongUrl == longUrl)
            {
                while (current != null)
                {
                    shortUrls.Add(current.TinyUrl);
                    current = current.Next;
                }
            }
            current = current?.Next;
        }
        return shortUrls;
    }

	/// <summary>
	/// Searches for the long URL associated with a given short URL.
	/// </summary>
	/// <param name="tinyUrl">The short URL to search for.</param>
	/// <returns>The long URL associated with the short URL.</returns>
    public string SearchByShortUrl(string tinyUrl)
    {
        foreach (UrlNode node in _buckets)
        {
            UrlNode current = node;
            while (current != null)
            {
                if (current.TinyUrl == tinyUrl)
                {
                    // increment the access count
                    current.AccessCount++;
                    return current.LongUrl;
                }
                current = current.Next;
            }
        }
        return null;
    }

	/// <summary>
	/// Gets the access count for a given short URL.
	/// </summary>
	/// <param name="tinyUrl">The short URL to search for.</param>
	/// <returns>The access count for the short URL.</returns>
    public int GetAccessCount(string tinyUrl)
    {
        foreach (UrlNode node in _buckets)
        {
            UrlNode current = node;
            while (current != null)
            {
                if (current.TinyUrl == tinyUrl)
                {
                    return current.AccessCount;
                }
                current = current.Next;
            }
        }
        return 0;
    }

    public bool Delete(string tinyUrl)
    {
        for (int i = 0; i < _buckets.Length; i++)
        {
            UrlNode current = _buckets[i];
            UrlNode previous = null;
            while (current != null)
            {
                if (current.TinyUrl == tinyUrl)
                {
                    if (previous == null)
                    {
                        _buckets[i] = current.Next;
                    }
                    else
                    {
                        previous.Next = current.Next;
                    }
                    return true;
                }
                previous = current;
                current = current.Next;
            }
        }
        return false;
    }
}
