namespace BattleSpace.Lib;


public class GameQueueGetCommandStrategy : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        var queue = (Queue<ICommand>)args[0];

        if (!queue.TryDequeue(out ICommand? cmd)) 
            throw new Exception();

        return cmd;
    }
}
