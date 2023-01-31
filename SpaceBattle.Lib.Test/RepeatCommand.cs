namespace BattleSpace.Lib.Test;

public class RepeatCommand : IRepeatable {
    IUObject uobject;
    ICommand command;

    public RepeatCommand(IUObject uobject, ICommand command) {
        this.uobject = uobject;
        this.command = command;
    }

    public ICommand Repeat() {
        return command;
    }
}
