namespace BattleSpace.Lib.Test;

public class InjectCommand : IInjectable {
    private IUObject _obj;
    private ICommand _cmd;

    public InjectCommand(IUObject obj, ICommand cmd) {
        _obj = obj;
        _cmd = cmd;
    }

    public ICommand Inject() {
        return new EmptyCommand();
    }
}
