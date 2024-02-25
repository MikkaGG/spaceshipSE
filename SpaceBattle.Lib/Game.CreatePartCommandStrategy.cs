using Hwdtech;

namespace BattleSpace.Lib;

public class CreatePartCommandStrategy : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        var name = (string) args[0];
        var obj = (IUObject) args[1];

        var list_string = IoC.Resolve<IEnumerable<string>>("SetupOperation." + name);
        var list_commands = list_string.Select(str => IoC.Resolve<ICommand>(str, obj));

        var macro = IoC.Resolve<ICommand>("CreateMacroCommand", list_commands);

        return macro;
    }
}
