using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib;

public class StartMoveCommand : ICommand {
    private IMoveCommandStartable startCommand;

    public StartMoveCommand(IMoveCommandStartable startCommand) {
        this.startCommand = startCommand;
    }
    
    public void Execute() {
        IoC.Resolve<ICommand>("Object.SetProperty", startCommand.uObject, "velocity", startCommand.velocity).Execute();
        var moveCommand = IoC.Resolve<ICommand>("Command.Move", startCommand.uObject);
        IoC.Resolve<ICommand>("Queue.Push", startCommand.queue, moveCommand).Execute();
    }
}
