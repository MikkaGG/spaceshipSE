namespace BattleSpace.Lib;
using Hwdtech;
using Hwdtech.Ioc;

public class EndMoveCommand : ICommand {
    public IMoveCommandEndable obj;
    public EndMoveCommand(IMoveCommandEndable obj) {
        this.obj = obj;
    }
    public void Execute() {
        var uobject = obj.UObject;
        var cmd = obj.Cmd;
        var properties = obj.Properties;
        IoC.Resolve<ICommand>("Game.Commands.DeleteObjectPropertyCommand", uobject, properties).Execute();
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<IInjectable>("Game.Commands.EmptyCommand", uobject, cmd).Inject());
    }
}
