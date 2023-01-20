using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace BattleSpace.Lib.Test;

public class TestMacroCommand {
    public TestMacroCommand() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var List = new Mock<IStrategy>();
        List.Setup(m => m.ExecuteStrategy()).Returns(new List<string> { "Command" });
        var Command = new Mock<ICommand>();
        Command.Setup(m => m.Execute());
        var Strategy = new Mock<IStrategy>();
        Strategy.Setup(m => m.ExecuteStrategy(It.IsAny<object[]>())).Returns(Command.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Operation.Move", (object[] args) => List.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Command", (object[] args) => Strategy.Object.ExecuteStrategy(args)).Execute();
    }

    [Fact]
    public void PositiveMacroCommandTest() {
        var CMacroCommand = new MacroCommandStrategy();
        var Obj = new Mock<IUObject>();
        var MacroCommand = (ICommand)CMacroCommand.ExecuteStrategy(Obj.Object, "Move");
        MacroCommand.Execute();
    }
}
