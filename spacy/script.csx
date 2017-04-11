class MyScript : ScriptCS
{
    public int Main()
    {
        return 5;
    }
}

interface ScriptCS
{
    int Main();
}

var ret = new MyScript().Main();
