using lab_3;

SymbolTable symbolTable = new(123);

Tuple<int, int> p1 = Tuple.Create(-1, -1);
Tuple<int, int> p2 = Tuple.Create(-1, -1);
Tuple<int, int> p3 = Tuple.Create(-1, -1);

try
{
    p1 = symbolTable.AddIdentifier("abc");

    Console.WriteLine("abc -> " + p1);
    Console.WriteLine("c -> " + symbolTable.AddIdentifier("c"));
    Console.WriteLine("a -> " + symbolTable.AddIdentifier("a"));
    Console.WriteLine("bc -> " + symbolTable.AddIdentifier("bc"));
    Console.WriteLine("ba -> " + symbolTable.AddIdentifier("ba"));

    Console.WriteLine("2 -> " + symbolTable.AddIntConstant(2));
    Console.WriteLine("3 -> " + symbolTable.AddIntConstant(3));

    p2 = symbolTable.AddIntConstant(100);

    Console.WriteLine("100 -> " + p2);
    Console.WriteLine("20 -> " + symbolTable.AddIntConstant(20));
    Console.WriteLine("131 -> " + symbolTable.AddIntConstant(131));
    Console.WriteLine("49 -> " + symbolTable.AddIntConstant(49));
    Console.WriteLine("96 -> " + symbolTable.AddIntConstant(96));

    Console.WriteLine("string1 -> " + symbolTable.AddStringConstant("string1"));
    Console.WriteLine("another -> " + symbolTable.AddStringConstant("another"));
    Console.WriteLine("lab -> " + symbolTable.AddStringConstant("lab"));
    Console.WriteLine("hello -> " + symbolTable.AddStringConstant("hello"));

    p3 = symbolTable.AddStringConstant("world");

    Console.WriteLine("world -> " + p3);
    Console.WriteLine("abc -> " + symbolTable.AddIdentifier("abc"));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine(symbolTable);

try
{
    if (!symbolTable.GetPositionIdentifier("abc").Equals(p1))
    {
        throw new Exception("abc does not have position" + p1);
    }

    if (!symbolTable.GetPositionIntConstant(100).Equals(p2))
    {
        throw new Exception("100 does not have position" + p2);
    }

    if (!symbolTable.GetPositionStringConstant("world").Equals(p3))
    {
        throw new Exception("world does not have position" + p3);
    }

    if (!symbolTable.GetPositionIdentifier("aaaa").Equals(Tuple.Create(-1, -1)))
    {
        throw new Exception("aaaa exists in the table");
    }

    if (!symbolTable.GetPositionIntConstant(96).Equals(Tuple.Create(2, 2)))
    {
        throw new Exception("96 does not have position (2, 2)");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("49 -> " + symbolTable.GetPositionIntConstant(49));

    if (!symbolTable.GetPositionIntConstant(49).Equals(Tuple.Create(2, 2)))
    {
        throw new Exception("49 does not have position (2, 2)");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("ba -> " + symbolTable.GetPositionIdentifier("ba"));

    if (!symbolTable.GetPositionIdentifier("ba").Equals(p1))
    {
        throw new Exception("ba does not have position" + p1);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("22 -> " + symbolTable.GetPositionIntConstant(22));

    if (!symbolTable.GetPositionIntConstant(22).Equals(p2))
    {
        throw new Exception("22 does not have position" + p2);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("word -> " + symbolTable.GetPositionStringConstant("word"));

    if (!symbolTable.GetPositionStringConstant("word").Equals(p3))
    {
        throw new Exception("word does not have position" + p3);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}