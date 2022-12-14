namespace BattleSpace.Lib;

public class RotateCommand : ICommand {
    private readonly IRotatable _rotatable;

    public RotateCommand(IRotatable rotatable) {
        _rotatable = rotatable;
    }

    public void Execute() {
        _rotatable.Angle = _rotatable.Angle + _rotatable.AngleVelocity;
    }
}
