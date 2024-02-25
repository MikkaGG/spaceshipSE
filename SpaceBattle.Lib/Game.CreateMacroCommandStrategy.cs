namespace BattleSpace.Lib;

public class CreateMacroCommandStrategy : IStrategy
{

    public object ExecuteStrategy(params object[] args)
    {
        return new MacroCommand((IEnumerable<ICommand>) args[0]);
    }
}
