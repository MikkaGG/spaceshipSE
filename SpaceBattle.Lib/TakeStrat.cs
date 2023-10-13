namespace BattleSpace.Lib;

public class TakeStrat : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        Queue<ICommand> q = (Queue<ICommand>)args[0];
        ICommand cmd = q.Dequeue();

        return cmd;
    }
}
