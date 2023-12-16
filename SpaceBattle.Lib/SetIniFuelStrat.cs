namespace BattleSpace.Lib;

public class SetIniFuelStrat : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        IUObject patient = (IUObject)args[0];
        return new SetIniFuelCMD(patient);
    }
}
