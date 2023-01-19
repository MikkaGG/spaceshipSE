using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib;

public class StartMoveCommand : ICommand {
    private IMoveCommandStartable startCommand;

    public StartMoveCommand(IMoveCommandStartable startCommand) {
        this.startCommand = startCommand;
    }
    
    public void Execute() {
        var moveCommand = IoC.Resolve<ICommand>("Command.Move", startCommand.uObject);
        IoC.Resolve<ICommand>("Object.SetProperty", startCommand.uObject, "velocity", startCommand.velocity).Execute();
        var create_queue = IoC.Resolve<Queue<ICommand>>("Queue");
        IoC.Resolve<ICommand>("Queue.Push", create_queue, moveCommand).Execute();
    }
}
