namespace BattleSpace.Lib;

public class MoveCommand : ICommand {
    private IMovable _imovable;

    public MoveCommand(IMovable imovable) {
        _imovable = imovable;
    }

    public void Execute() {
        _imovable.Position = _imovable.Position + _imovable.Velocity;
    }
}
