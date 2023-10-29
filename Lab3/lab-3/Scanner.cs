using System.Text.RegularExpressions;

namespace lab_3;

public class Scanner
{
    private string _program;
    private int _index = 0;
    private int _currentLine = 1;

    private readonly List<string> _tokens; // will keep track of the symbols that are allowed within the language's syntax rules (ex: a).
    private readonly List<string> _reservedWords; // will keep track of the reserved words (ex: int).

    private SymbolTable _symbolTable; // will keep track of identifiers, string constants and int constants.
    private List<Tuple<string, Tuple<int, int>>> _PIF; // program internal form -> keeps track of tokens.

    public Scanner()
    {
        _program = string.Empty;
        _symbolTable = new SymbolTable(100);
        _PIF = new List<Tuple<string, Tuple<int, int>>>();
        _reservedWords = new List<string>();
        _tokens = new List<string>();

        try
        {
            ReadTokensAndReservedWords();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    // Sets the program that will be executed to a given string.
    public void SetProgram(string program)
    {
        _program = program;
    }

    // Reads the characters from the file token.in and classyfies them between reserved words and tokens.
    private void ReadTokensAndReservedWords()
    {
        string[] tokenLines = File.ReadAllLines("../../../resources/token.in");

        foreach (string line in tokenLines)
        {
            string[] parts = line.Split(' ');

            switch (parts[0])
            {
                case "function":
                case "int":
                case "string":
                case "bool":
                case "list":
                case "and":
                case "or":
                case "read":
                case "write":
                case "if":
                case "else":
                case "while":
                case "true":
                case "false":
                    _reservedWords.Add(parts[0]);
                    break;

                default:
                    _tokens.Add(parts[0]);
                    break;
            }
        }
    }

    // Skips all the spaces from the current index until the next valid character.
    private bool SkipSpaces()
    {
        int initialIndex = _index;

        while (_index < _program.Length && char.IsWhiteSpace(_program[_index]))
        {
            if (_program[_index] == '\n')
            {
                _currentLine++;
            }

            _index++;
        }

        return initialIndex != _index;
    }

    // Skips all the comments from the current index until the next valid character.
    private bool SkipComments()
    {
        int initialIndex = _index;

        while (_index < _program.Length && _program[_index] == '#')
        {
            while (_index < _program.Length && _program[_index] != '\n')
            {
                _index++;
            }
        }

        return initialIndex != _index;
    }

    // Treats the case when the current analysed group of characters is a string constant.
    private bool TreatStringConstant()
    {
        var regexForStringConstant = new Regex("^\"[a-zA-z0-9_ ?:*^+=.!]*\"");
        var match = regexForStringConstant.Match(_program[_index..]);

        if (!match.Success)
        {
            if (new Regex("^\"[^\"]").Match(_program[_index..]).Success) // checks that string is enclosed in " "
            {
                throw new ScannerException("Missing \" at line " + _currentLine);
            }

            return false;
        }

        var stringConstant = match.Value;
        _index += stringConstant.Length;
        Tuple<int, int> position;

        try
        {
            position = _symbolTable.AddStringConstant(stringConstant);
        }
        catch (Exception)
        {
            position = _symbolTable.GetPositionStringConstant(stringConstant);
        }

        _PIF.Add(new Tuple<string, Tuple<int, int>>("string const", position));

        return true;
    }

    // Treats the case when the current analysed group of characters is a int constant.
    private bool TreatIntConstant()
    {
        var regexForIntConstant = new Regex("^([+-]?[1-9][0-9]*|0)");
        var match = regexForIntConstant.Match(_program[_index..]);

        if (!match.Success)
        {
            return false;
        }

        if (new Regex("^([+-]?[1-9][0-9]*|0)[a-zA-z_]").Match(_program[_index..]).Success) // checks there are no characters
        {
            return false;
        }

        var intConstant = match.Groups[1].Value;
        _index += intConstant.Length;
        Tuple<int, int> position;

        try
        {
            position = _symbolTable.AddIntConstant(int.Parse(intConstant));
        }
        catch (Exception)
        {
            position = _symbolTable.GetPositionIntConstant(int.Parse(intConstant));
        }

        _PIF.Add(new Tuple<string, Tuple<int, int>>("int const", position));

        return true;
    }

    // Checks if the possibleIndentifier is part of the reserved keywords.
    private bool CheckIfIdentifierIsValid(string possibleIdentifier)
    {
        if (_reservedWords.Contains(possibleIdentifier))
        {
            return false;
        }

        return true;
    }

    // Treats the case when the current analysed group of characters is an identifier.
    private bool TreatIdentifier()
    {
        var regexForIdentifier = new Regex("^([a-zA-Z_][a-zA-Z0-9_]*)");
        var match = regexForIdentifier.Match(_program[_index..]);

        if (!match.Success)
        {
            return false;
        }

        var identifier = match.Groups[1].Value;

        if (!CheckIfIdentifierIsValid(identifier)) // checks that identifier is not a reserved word.
        {
            return false;
        }

        _index += identifier.Length;
        Tuple<int, int> position;

        try
        {
            position = _symbolTable.AddIdentifier(identifier);
        }
        catch (Exception)
        {
            position = _symbolTable.GetPositionIdentifier(identifier);
        }

        _PIF.Add(new Tuple<string, Tuple<int, int>>("identifier", position));

        return true;
    }

    // Treats the case when the current analysed group of characters is a token from token.in.
    private bool TreatFromTokenList()
    {
        string possibleToken = _program[_index..].Split(' ')[0];

        foreach (var reservedToken in _reservedWords) // checks if it is a reserved word.
        {
            if (possibleToken.StartsWith(reservedToken))
            {
                var regex = "^" + "[a-zA-Z0-9_]*" + reservedToken + "[a-zA-Z0-9_]+";

                if (new Regex(regex).IsMatch(possibleToken))
                {
                    return false;
                }

                _index += reservedToken.Length;
                _PIF.Add(new Tuple<string, Tuple<int, int>>(reservedToken, new Tuple<int, int>(-1, -1)));

                return true;
            }
        }

        foreach (var token in _tokens)
        {
            if (string.Equals(token, possibleToken)) // checks if it is a token.
            {
                _index += token.Length;
                _PIF.Add(new Tuple<string, Tuple<int, int>>(token, new Tuple<int, int>(-1, -1)));

                return true;
            }
            else if (possibleToken.StartsWith(token))
            {
                _index += token.Length;

                possibleToken = possibleToken.TrimEnd(',', ';');

                if (char.IsDigit(possibleToken[0]) && possibleToken.Length > 1 && char.IsLetter(possibleToken[1]))
                {
                    return false;
                }

                _PIF.Add(new Tuple<string, Tuple<int, int>>(token, new Tuple<int, int>(-1, -1)));

                return true;
            }
        }

        return false;
    }

    // Analyses and classyfies the next token.
    private void NextToken()
    {
        while (SkipSpaces() || SkipComments()) { }

        if (_index == _program.Length)
        {
            return;
        }

        if (TreatIdentifier())
        {
            return;
        }

        if (TreatStringConstant())
        {
            return;
        }

        if (TreatIntConstant())
        {
            return;
        }

        if (TreatFromTokenList())
        {
            return;
        }

        throw new ScannerException("Lexical error: invalid token at line " + _currentLine + ", index " + _index);
    }

    // Scans the given program as a string and lexically analyses it.
    public void Scan(string programFileName)
    {
        try
        {
            string filePath = Path.Combine("../../../resources", programFileName);
            SetProgram(File.ReadAllText(filePath));

            _index = 0;
            _PIF = new List<Tuple<string, Tuple<int, int>>>();
            _symbolTable = new SymbolTable(100);
            _currentLine = 1;

            while (_index < _program.Length)
            {
                NextToken();
            }

            using (StreamWriter pifFileWriter = new(Path.Combine("../../../outputs", "PIF" + programFileName.Replace(".txt", ".out"))))
            {
                foreach (var pair in _PIF)
                {
                    pifFileWriter.WriteLine($"{pair.Item1} -> ({pair.Item2.Item1}, {pair.Item2.Item2})");
                }
            }

            using (StreamWriter stFileWriter = new(Path.Combine("../../../outputs", "ST" + programFileName.Replace(".txt", ".out"))))
            {
                stFileWriter.Write(_symbolTable.ToString());
            }

            Console.WriteLine("Lexically correct");
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
