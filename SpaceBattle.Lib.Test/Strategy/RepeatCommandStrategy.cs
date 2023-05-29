namespace BattleSpace.Lib.Test;

public class RepeatCommandStrategy : IStrategy {
    public object ExecuteStrategy(params object[] args) {
        var obj = (IUObject)args[0];
        var command = (ICommand)args[1];

        return new RepeatCommand(obj, command);
    }
}
