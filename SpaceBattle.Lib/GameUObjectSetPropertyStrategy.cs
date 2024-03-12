namespace BattleSpace.Lib;

public class GameUObjectSetPropertyStrategy : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        var key = (string)args[1];
        var value = args[2];

        return new GameUObjectSetPropertyCommand(obj, key, value);
    }
}
