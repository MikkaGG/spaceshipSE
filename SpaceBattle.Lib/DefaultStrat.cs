namespace BattleSpace.Lib;

public class DefaultStrat : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        throw (Exception)Activator.CreateInstance(args[0].GetType());
    }
}
