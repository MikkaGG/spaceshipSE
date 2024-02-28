namespace BattleSpace.Lib;


public class CreateAdapterBuilderStrategy : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        var adaptableType = (Type)args[0];
        var adaptiveType = (Type)args[1];

        return new AdapterBuilder(adaptableType, adaptiveType);
    }
}
