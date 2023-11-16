using System.Text.RegularExpressions;
using System.Text;

namespace lab_4;

public class FA
{
    private List<string> _states; // list of all possible states
    private List<string> _alphabet; // list of the characters that can be used (alphabet)
    private readonly List<Transition> _transitions; // list of possible transitions between states

    private string _initialState; // the initial state
    private List<string> _outputStates; // the output states

    private readonly string _filename; // the name of the file from which the FA will be initialised

    public FA(string filename)
    {
        _filename = filename;
        _states = new List<string>();
        _alphabet = new List<string>();
        _transitions = new List<Transition>();
        _initialState = "";
        _outputStates = new List<string>();

        try
        {
            Initialize();
        }
        catch (Exception)
        {
            Console.WriteLine("Error when initializing FA");
        }
    }

    // initialise the states, alphabet, out states, initial state and transitions
    private void Initialize()
    {
        var regex = new Regex("^([a-z_]*)="); // checks that the line starts with a string (for example states=...)

        foreach (var line in File.ReadLines(_filename))
        {
            var matcher = regex.Match(line);

            if (!matcher.Success)
            {
                throw new Exception("Invalid line: " + line);
            }

            switch (matcher.Groups[1].Value)
            {
                case "states": // save the states
                    var statesWithCurlyBrackets = line.Substring(line.IndexOf("=") + 1);
                    var states = statesWithCurlyBrackets.Substring(1, statesWithCurlyBrackets.Length - 2).Trim(); // go for -2 to get rid of } at the end
                    _states = states.Split(", ").ToList();
                    break;

                case "alphabet": // save the alphabet
                    var alphabetWithCurlyBrackets = line.Substring(line.IndexOf("=") + 1);
                    var alphabet = alphabetWithCurlyBrackets.Substring(1, alphabetWithCurlyBrackets.Length - 2).Trim(); // go for -2 to get rid of } at the end
                    _alphabet = alphabet.Split(", ").ToList();
                    break;

                case "out_states": // save the output states
                    var outputStatesWithCurlyBrackets = line.Substring(line.IndexOf("=") + 1);
                    var outputStates = outputStatesWithCurlyBrackets.Substring(1, outputStatesWithCurlyBrackets.Length - 2).Trim(); // go for -2 to get rid of } at the end
                    _outputStates = outputStates.Split(", ").ToList();
                    break;

                case "initial_state": // save the initial state
                    _initialState = line.Substring(line.IndexOf("=") + 1).Trim();
                    break;

                case "transitions": // save the transitions
                    var transitionsWithCurlyBrackets = line.Substring(line.IndexOf("=") + 1);
                    var transitions = transitionsWithCurlyBrackets.Substring(1, transitionsWithCurlyBrackets.Length - 2).Trim(); // go for -2 to get rid of } at the end
                    var transitionsList = transitions.Split("; ").ToList(); // split the transitions into a list

                    foreach (var transition in transitionsList)
                    {
                        var transitionWithoutParantheses = transition.Substring(1, transition.Length - 2).Trim(); // go for -2 to get rid of ) at the end
                        var individualValues = transitionWithoutParantheses.Split(", ");
                        _transitions.Add(new Transition(individualValues[0], individualValues[1], individualValues[2]));
                    }
                    break;

                default:
                    throw new Exception("Invalid line in file");
            }
        }

        if (IsDFA() == false)
        {
            throw new Exception();
        }

        Console.Write("Is DFA\n\n");
    }

    // helper function to print a list of strings (used for states, output states and alphabet)
    private static void PrintList(string listname, List<string> list)
    {
        Console.Write(listname + " = {");

        for (int i = 0; i < list.Count; i++)
        {
            if (i != list.Count - 1)
            {
                Console.Write(list[i] + ", ");
            }
            else
            {
                Console.Write(list[i]);
            }
        }

        Console.WriteLine("}");
    }

    // print the states using the PrintList function
    public void PrintStates()
    {
        PrintList("states", _states);
    }

    // print the alphabet using the PrintList function
    public void PrintAlphabet()
    {
        PrintList("alphabet", _alphabet);
    }

    // print the output states using the PrintListOfStrings function
    public void PrintOutputStates()
    {
        PrintList("out_states", _outputStates);
    }

    // print the initial state
    public void PrintInitialState()
    {
        Console.WriteLine("initial_state = " + _initialState);
    }

    // print the transitions
    public void PrintTransitions()
    {
        Console.Write("transitions = {");

        for (int i = 0; i < _transitions.Count; i++)
        {
            if (i != _transitions.Count - 1)
            {
                Console.Write("(" + _transitions[i].From + ", " + _transitions[i].To + ", " + _transitions[i].Label + "); ");
            }
            else
            {
                Console.Write("(" + _transitions[i].From + ", " + _transitions[i].To + ", " + _transitions[i].Label + ")");
            }
        }

        Console.WriteLine("}");
    }

    // check the if the inserted word is accepted by the FA, if the transitions lead to a state that is in output states
    public bool CheckAccepted(string word) // daca nu gaseste tranzitie in R 1 si state ul ramane R trebuie acceptat?
    {
        List<string> wordAsList = word.Select(c => c.ToString()).ToList(); // transform the word into a list of characters
        var currentState = _initialState; // get initial state

        foreach (var c in wordAsList)
        {
            var found = false; // set to false because currently we did not find any transition

            foreach (var transition in _transitions) // check if there is a possible transition
            {
                if (transition.From == currentState && transition.Label == c)
                {
                    currentState = transition.To;
                    found = true; // transition was found
                    break;
                }
            }

            if (!found)
            {
                return false; // if no transition is found then the word is not accepted
            }
        }

        return _outputStates.Contains(currentState); // if after processing the state is in the output states, than the word is accepted
    }

    // Check that is DFA
    private bool IsDFA()
    {
        if (_states.Count == 0 || _alphabet.Count == 0 || _outputStates.Count == 0)
        {
            return false;
        }

        if (!_states.Contains(_initialState))
        {
            return false;
        }

        if (!_outputStates.All(_states.Contains))
        {
            return false;
        }

        HashSet<string> seenTransitions = new();

        foreach (Transition transition in _transitions)
        {
            string key = $"{transition.From},{transition.Label}";

            if (seenTransitions.Contains(key))
            {
                return false;
            }

            seenTransitions.Add(key);
        }

        foreach (string state in _states)
        {
            HashSet<string> seenLabels = new();

            foreach (Transition transition in _transitions.Where(t => t.From.Equals(state)))
            {
                string label = transition.Label;

                if (seenLabels.Contains(label))
                {
                    return false;
                }

                seenLabels.Add(label);
            }

            if (!seenLabels.All(_alphabet.Contains))
            {
                return false;
            }
        }


        return true;
    }

}
