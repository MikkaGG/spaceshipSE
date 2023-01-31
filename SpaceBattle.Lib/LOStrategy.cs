using Hwdtech;
namespace BattleSpace.Lib;
public class LOstrategy : IStrategy {
    public object ExecuteStrategy(params object[] args){
        string name = (string)args[0];
        IUObject obj = (IUObject)args[1];
        var Command = IoC.Resolve<ICommand>("Game.Command.CreateCommand", name, obj);
        ICommand RepeatCommand = IoC.Resolve<ICommand>("Game.Command.RepeatCommand", Command);
        ICommand InjectCommand = IoC.Resolve<ICommand>("Game.Command.InjectCommand", RepeatCommand);
        return InjectCommand;
    }
}
