namespace BattleSpace.Lib;

public class ActionCommand : ICommand
{
    Action<object[]> act;
    object[] args;

    public ActionCommand(Action<object[]> _act, params object[] _args)
    {
        this.act = _act;
        this.args = _args;
    }

    public void Execute()
    {
        act(args);
    }
}
