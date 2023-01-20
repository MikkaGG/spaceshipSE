using Hwdtech;
namespace BattleSpace.Lib;
public class LOstrategy : IStrategy {
    public object ExecuteStrategy(params object[] args){
        string name = (string)args[0];
        IUObject obj = (IUObject)args[1];
        var MacroCommand = IoC.Resolve<ICommand>("BuildMacroCommandStrat", name, obj);
        ICommand RepeatCommand = IoC.Resolve<ICommand>("Game.RepeadCommand", MacroCommand);
        ICommand InjectCommand = IoC.Resolve<ICommand>("Game.InjectCommand", RepeatCommand);
        return InjectCommand;
    }
}
