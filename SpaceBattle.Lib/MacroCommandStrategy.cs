using Hwdtech;

namespace BattleSpace.Lib;
public class MacroCommandStrategy: IStrategy { 
    public object ExecuteStrategy(params object[] args) {
        var obj = (IUObject)args[0];
        var name = (string)args[1];

        var dependencies = IoC.Resolve<List<string>>("Game.Operation." + name);

        var list_cmds = new List<ICommand>();

        foreach (var dependency in dependencies) {
            list_cmds.Add(IoC.Resolve<ICommand>(dependency, obj));
        }

        return new MacroCommand(list_cmds);
    }
}
