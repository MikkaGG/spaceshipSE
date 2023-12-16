using Hwdtech;

namespace BattleSpace.Lib;

public class SetIniPosCMD : ICommand
{
    IUObject patient;

    public SetIniPosCMD(IUObject patient)
    {
        this.patient = patient;
    }

    public void Execute()
    {
        Vector coords = IoC.Resolve<Vector>("Game.IniPosIter.Next");
        IoC.Resolve<ICommand>("Game.UObject.Set", patient, "position", coords).Execute();
    }
}
