using lab_3;

SymbolTable symbolTable = new(123);

Tuple<int, int> p1 = Tuple.Create(-1, -1);
Tuple<int, int> p2 = Tuple.Create(-1, -1);
Tuple<int, int> p3 = Tuple.Create(-1, -1);

try
{
    p1 = symbolTable.AddIdentifier("dragos");

    Console.WriteLine("dragos -> " + p1);
    Console.WriteLine("a -> " + symbolTable.AddIdentifier("a"));
    Console.WriteLine("b -> " + symbolTable.AddIdentifier("b"));
    Console.WriteLine("b -> " + symbolTable.AddIdentifier("c"));
    Console.WriteLine("ab -> " + symbolTable.AddIdentifier("ab"));
    Console.WriteLine("bc -> " + symbolTable.AddIdentifier("bc"));
    Console.WriteLine("ac -> " + symbolTable.AddIdentifier("ac"));

    Console.WriteLine("1 -> " + symbolTable.AddIntConstant(1));
    Console.WriteLine("2 -> " + symbolTable.AddIntConstant(2));
    Console.WriteLine("3 -> " + symbolTable.AddIntConstant(3));
    Console.WriteLine("4 -> " + symbolTable.AddIntConstant(4));
    Console.WriteLine("5 -> " + symbolTable.AddIntConstant(5));

    p2 = symbolTable.AddIntConstant(100);

    Console.WriteLine("100 -> " + p2);
    Console.WriteLine("20 -> " + symbolTable.AddIntConstant(20));
    Console.WriteLine("122 -> " + symbolTable.AddIntConstant(122));
    Console.WriteLine("245 -> " + symbolTable.AddIntConstant(245));
    Console.WriteLine("368 -> " + symbolTable.AddIntConstant(368));
    Console.WriteLine("96 -> " + symbolTable.AddIntConstant(96));

    Console.WriteLine("first-string -> " + symbolTable.AddStringConstant("first-string"));
    Console.WriteLine("another-string -> " + symbolTable.AddStringConstant("another-string"));
    Console.WriteLine("lab3 -> " + symbolTable.AddStringConstant("lab3"));
    Console.WriteLine("hello world -> " + symbolTable.AddStringConstant("hello world"));

    p3 = symbolTable.AddStringConstant("car");

    Console.WriteLine("car -> " + p3);
    Console.WriteLine("ac -> " + symbolTable.AddIdentifier("ac"));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    if (!symbolTable.GetPositionIdentifier("dragos").Equals(p1))
    {
        throw new Exception("dragos does not have position" + p1);
    }

    if (!symbolTable.GetPositionIntConstant(100).Equals(p2))
    {
        throw new Exception("100 does not have position" + p2);
    }

    if (!symbolTable.GetPositionStringConstant("car").Equals(p3))
    {
        throw new Exception("car does not have position" + p3);
    }

    if (!symbolTable.GetPositionIdentifier("m").Equals(Tuple.Create(-1, -1)))
    {
        throw new Exception("m exists in the table");
    }

    if (!symbolTable.GetPositionIntConstant(368).Equals(Tuple.Create(2, 2)))
    {
        throw new Exception("368 does not have position (2, 2)");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("368 -> " + symbolTable.GetPositionIntConstant(368));

    if (!symbolTable.GetPositionIntConstant(368).Equals(Tuple.Create(122, 2)))
    {
        throw new Exception("368 does not have position (122, 2)");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("ab -> " + symbolTable.GetPositionIdentifier("ab"));

    if (!symbolTable.GetPositionIdentifier("ab").Equals(Tuple.Create(72, 0)))
    {
        throw new Exception("ab does not have position" + p1);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("20 -> " + symbolTable.GetPositionIntConstant(20));

    if (!symbolTable.GetPositionIntConstant(20).Equals(Tuple.Create(20, 0)))
    {
        throw new Exception("20 does not have position" + p2);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

try
{
    Console.WriteLine("hello world -> " + symbolTable.GetPositionStringConstant("hello world"));

    if (!symbolTable.GetPositionStringConstant("hello world").Equals(Tuple.Create(9, 0)))
    {
        throw new Exception("hello word does not have position" + p3);
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine(symbolTable);