namespace BattleSpace.Lib;

public class SetIniPosStrat : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        IUObject patient = (IUObject) args[0];
        return new SetIniPosCMD(patient);
    }
}
