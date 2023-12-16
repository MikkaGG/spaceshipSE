using Hwdtech;

namespace BattleSpace.Lib;

public class SetIniFuelCMD : ICommand
{
    IUObject patient;

    public SetIniFuelCMD(IUObject patient)
    {
        this.patient = patient;
    }

    public void Execute()
    {
        int fuel = IoC.Resolve<int>("Game.IniFuelIter.Next");
        IoC.Resolve<ICommand>("Game.UObject.Set", patient, "fuel", fuel).Execute();
    }
}
