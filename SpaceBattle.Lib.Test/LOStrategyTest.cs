using Moq;
using Xunit;

using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test;

public class CreateLongTermOperationCommandTest {
    public CreateLongTermOperationCommandTest() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var createLOStrategy = new LOstrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.CreatLOCommand", (object[] args) => (createLOStrategy.ExecuteStrategy(args))).Execute();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.ExecuteStrategy(It.IsAny<string>(), It.IsAny<IUObject>())).Returns(It.IsAny<ICommand>());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.CreateCommand", (object[] args) => (createCommandStrategy.Object.ExecuteStrategy(args))).Execute();

        var repeatStrategy = new Mock<IStrategy>();
        repeatStrategy.Setup(s => s.ExecuteStrategy(It.IsAny<IUObject>(), It.IsAny<ICommand>())).Returns(It.IsAny<ICommand>());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.RepeatCommand", (object[] args) => (repeatStrategy.Object.ExecuteStrategy(args))).Execute();

        var injectStrategy = new Mock<IStrategy>();
        injectStrategy.Setup(s => s.ExecuteStrategy(It.IsAny<IUObject>(), It.IsAny<ICommand>())).Returns(It.IsAny<ICommand>());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.InjectCommand", (object[] args) => (injectStrategy.Object.ExecuteStrategy(args))).Execute();

        var plugStrategy = new Mock<IStrategy>();
        plugStrategy.Setup(s => s.ExecuteStrategy(It.IsAny<string>(), It.IsAny<IUObject>())).Returns(It.IsAny<ICommand>());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.PlugCommand", (object[] args) => (plugStrategy.Object.ExecuteStrategy(args))).Execute();
    }

    [Fact] 
     public void SuccessfulCreate() { 
        var dependence = "Game.Command.PlugCommand"; 
        var uobject = new Mock<IUObject>(); 

        var creatLOCommand = new CreatLOCommand(dependence, uobject.Object);
        creatLOCommand.Execute(); 
    } 
}
