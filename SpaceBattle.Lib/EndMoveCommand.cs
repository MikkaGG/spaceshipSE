namespace BattleSpace.Lib;
using Hwdtech;
public class EndMoveCommand : ICommand {
    public IMoveCommandEndable obj;
    public EndMoveCommand(IMoveCommandEndable obj) {
        this.obj = obj;
    }
    public void Execute() {
        var uobject = obj.UObject;
        var cmd = obj.Cmd;
        var vel = obj.velocity;
        IoC.Resolve<ICommand>("Game.Commands.DeleteObjectPropertyCommand", uobject, vel).Execute();
        var injectbleCommand = IoC.Resolve<IInjectable>("Game.Commands.EmptyCommand", uobject, cmd).Inject();
        IoC.Resolve<ICommand>("Game.Queue.Push", injectbleCommand).Execute();   
    }
}
