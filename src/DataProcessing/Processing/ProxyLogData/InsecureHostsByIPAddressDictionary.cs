using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace DataProcessing;

internal class InsecureHostsByIPAddressDictionary : IDictionary<IPAddress, HashSet<string>>
{
    private readonly Dictionary<IPAddress, HashSet<string>> _data;

    public InsecureHostsByIPAddressDictionary()
    {
        _data = new(new Dictionary<IPAddress, HashSet<string>>());
    }

    public InsecureHostsByIPAddressDictionary(Dictionary<IPAddress, HashSet<string>> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        _data = data;
    }

    public void AddWhenUnique(IPAddress ipAddress, string hostName)
    {
        if (_data.TryGetValue(ipAddress, out var insecureHosts))
        {
            insecureHosts.Add(hostName);
        }
        else
        {
            _data.Add(ipAddress, new HashSet<string> { hostName });
        }
    }

    public void Add(IPAddress key, HashSet<string> value)
    {
        ((IDictionary<IPAddress, HashSet<string>>)_data).Add(key, value);
    }

    public bool ContainsKey(IPAddress key)
    {
        return ((IDictionary<IPAddress, HashSet<string>>)_data).ContainsKey(key);
    }

    public bool Remove(IPAddress key)
    {
        return ((IDictionary<IPAddress, HashSet<string>>)_data).Remove(key);
    }

    public bool TryGetValue(IPAddress key, [MaybeNullWhen(false)] out HashSet<string> value)
    {
        return ((IDictionary<IPAddress, HashSet<string>>)_data).TryGetValue(key, out value);
    }

    public void Add(KeyValuePair<IPAddress, HashSet<string>> item)
    {
        ((ICollection<KeyValuePair<IPAddress, HashSet<string>>>)_data).Add(item);
    }

    public void Clear()
    {
        ((ICollection<KeyValuePair<IPAddress, HashSet<string>>>)_data).Clear();
    }

    public bool Contains(KeyValuePair<IPAddress, HashSet<string>> item)
    {
        return ((ICollection<KeyValuePair<IPAddress, HashSet<string>>>)_data).Contains(item);
    }

    public void CopyTo(KeyValuePair<IPAddress, HashSet<string>>[] array, int arrayIndex)
    {
        ((ICollection<KeyValuePair<IPAddress, HashSet<string>>>)_data).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<IPAddress, HashSet<string>> item)
    {
        return ((ICollection<KeyValuePair<IPAddress, HashSet<string>>>)_data).Remove(item);
    }

    public IEnumerator<KeyValuePair<IPAddress, HashSet<string>>> GetEnumerator()
    {
        return ((IEnumerable<KeyValuePair<IPAddress, HashSet<string>>>)_data).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_data).GetEnumerator();
    }

    public int Count => _data.Count;

    public ICollection<IPAddress> Keys => ((IDictionary<IPAddress, HashSet<string>>)_data).Keys;

    public ICollection<HashSet<string>> Values => ((IDictionary<IPAddress, HashSet<string>>)_data).Values;

    public bool IsReadOnly => ((ICollection<KeyValuePair<IPAddress, HashSet<string>>>)_data).IsReadOnly;

    public HashSet<string> this[IPAddress key] { get => ((IDictionary<IPAddress, HashSet<string>>)_data)[key]; set => ((IDictionary<IPAddress, HashSet<string>>)_data)[key] = value; }
}