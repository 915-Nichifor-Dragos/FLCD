using lab_4;

Scanner scanner = new();

string program1 = "p1.txt";
string program2 = "p2.txt";
string program3 = "p3.txt";
//string program1err = "p1err.txt";

scanner.Scan(program1);
scanner.Scan(program2);
scanner.Scan(program3);
//scanner.Scan(program1err);

//var fa = new FA("../../../resources/fa.in");
//var fa = new FA("../../../resources/identifier.in");
var fa = new FA("../../../resources/int_constants.in");
Console.WriteLine("1. Print states");
Console.WriteLine("2. Print alphabet");
Console.WriteLine("3. Print output states");
Console.WriteLine("4. Print initial state");
Console.WriteLine("5. Print transitions");
Console.WriteLine("6. Check word");
Console.WriteLine("0. Exit");

while (true)
{
    Console.Write("<> ");
    var option = Convert.ToInt32(Console.ReadLine());

    switch (option)
    {
        case 1:
            fa.PrintStates();
            break;

        case 2:
            fa.PrintAlphabet();
            break;

        case 3:
            fa.PrintOutputStates();
            break;

        case 4:
            fa.PrintInitialState();
            break;

        case 5:
            fa.PrintTransitions();
            break;

        case 6:
            Console.Write("Enter word: ");

            var word = Console.ReadLine();
            Console.WriteLine(fa.CheckAccepted(word));
            break;

        case 0:
            return;

        default:
            Console.WriteLine("Invalid option");
            break;
    }
}