namespace BattleSpace.Lib;

public interface IReceiver
{
    public ICommand Receive();
    public bool isEmpty();
}
