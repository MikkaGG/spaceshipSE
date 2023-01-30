namespace BattleSpace.Lib.Test;

public class InjectCommand : IInjectable {
    IUObject _obj;
    ICommand _cmd;

    public InjectCommand(IUObject obj, ICommand cmd) {
        _obj = obj;
        _cmd = cmd;
    }

    public ICommand Inject() {
        var cmd = new EmptyCommand();
        return cmd;
    }
}
