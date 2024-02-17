namespace BattleSpace.Lib;

public interface ISender
{
    public void Send(ICommand message);
}
