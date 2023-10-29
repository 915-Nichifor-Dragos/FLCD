namespace lab_3;

public class HashTable<T>
{
    private readonly int _size;

    private readonly List<List<T>> _items;

    public HashTable(int size)
    {
        _size = size;
        _items = new List<List<T>>(size);

        for (int i = 0; i < size; i++)
        {
            _items.Add(new List<T>());
        }
    }

    private int Hash(int key)
    {
        return key % _size;
    }

    private int Hash(string key)
    {
        int sum = 0;

        foreach (char c in key)
        {
            sum += c;
        }

        return sum % _size;
    }

    public int Size()
    {
        return _size;
    }

    private int GetHashValue(T key)
    {
        int hashValue = -1;

        if (key is int)
        {
            hashValue = Hash((int)(object)key);
        }

        else if (key is string)
        {
            hashValue = Hash((string)(object)key);
        }

        return hashValue;
    }

    public Tuple<int, int> Add(T key)
    {
        int hashValue = GetHashValue(key);

        if (!_items[hashValue].Contains(key))
        {
            _items[hashValue].Add(key);

            return new Tuple<int, int>(hashValue, _items[hashValue].IndexOf(key));
        }

        throw new Exception("Key " + key + " is already in the table!");
    }

    public bool Contains(T key)
    {
        int hashValue = GetHashValue(key);

        return _items[hashValue].Contains(key);
    }

    public Tuple<int, int> GetPosition(T key)
    {
        if (Contains(key))
        {
            int hashValue = GetHashValue(key);

            return new Tuple<int, int>(hashValue, _items[hashValue].IndexOf(key));
        }

        return new Tuple<int, int>(-1, -1);
    }

    public override string ToString()
    {
        return "HashTable{" + "items=" + _items + '}';
    }
}