using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib;

public class CreatLOCommand : ICommand {
    public string dependence;
    public IUObject uobject;

    public CreatLOCommand(string dependence, IUObject uobject) {
        this.dependence = dependence;
        this.uobject = uobject;
    }

    public void Execute() {
        IoC.Resolve<ICommand>("Game.Command.CreatLOCommand", dependence, uobject);
    }
}
