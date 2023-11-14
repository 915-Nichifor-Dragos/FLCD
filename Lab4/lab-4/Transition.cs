namespace lab_4;

public class Transition
{
    private string _from; // from what state
    private string _to; // to which state
    private string _label; // using what character (label)

    public Transition(string from, string to, string label)
    {
        _from = from;
        _to = to;
        _label = label;
    }

    public string From
    {
        get { return _from; }
        set { _from = value; }
    }

    public string To
    {
        get { return _to; }
        set { _to = value; }
    }

    public string Label
    {
        get { return _label; }
        set { _label = value; }
    }
}