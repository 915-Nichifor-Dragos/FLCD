namespace lab_3;

public class SymbolTable
{
    private readonly int _size;

    private readonly HashTable<string> _identifiersHashTable;
    private readonly HashTable<int> _intConstantsHashTable;
    private readonly HashTable<string> _stringConstantsHashTable;

    public SymbolTable(int size)
    {
        _size = size;
        _identifiersHashTable = new HashTable<string>(_size);
        _intConstantsHashTable = new HashTable<int>(_size);
        _stringConstantsHashTable = new HashTable<string>(_size);
    }

    public Tuple<int, int> AddIdentifier(string name)
    {
        return _identifiersHashTable.Add(name);
    }

    public Tuple<int, int> AddIntConstant(int constant)
    {
        return _intConstantsHashTable.Add(constant);
    }

    public Tuple<int, int> AddStringConstant(string constant)
    {
        return _stringConstantsHashTable.Add(constant);
    }

    public bool HasIdentifier(string name)
    {
        return _identifiersHashTable.Contains(name);
    }

    public bool HasIntConstant(int constant)
    {
        return _intConstantsHashTable.Contains(constant);
    }

    public bool HasStringConstant(string constant)
    {
        return _stringConstantsHashTable.Contains(constant);
    }

    public Tuple<int, int> GetPositionIdentifier(string name)
    {
        return _identifiersHashTable.GetPosition(name);
    }

    public Tuple<int, int> GetPositionIntConstant(int constant)
    {
        return _intConstantsHashTable.GetPosition(constant);
    }

    public Tuple<int, int> GetPositionStringConstant(string constant)
    {
        return _stringConstantsHashTable.GetPosition(constant);
    }

    public override string ToString()
    {
        return "SymbolTable{" +
               "identifiersHashTable=" + _identifiersHashTable +
               "\nintConstantsHashTable=" + _intConstantsHashTable +
               "\nstringConstantsHashTable=" + _stringConstantsHashTable +
               '}';
    }
}