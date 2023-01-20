using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace BattleSpace.Lib.Test;
public class LongTermCommandTests {
    public LongTermCommandTests() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var MockCmd = new Mock<ICommand>();
        MockCmd.Setup(mc => mc.Execute());
        var MockStrategyParams = new Mock<IStrategy>();
        MockStrategyParams.Setup(mc => mc.ExecuteStrategy(It.IsAny<object[]>())).Returns(MockCmd.Object);
        var MockEnStr = new Mock<IEnumerable<string>>();
        var MockStratReturnStr = new Mock<IStrategy>();
        MockStratReturnStr.Setup(mc => mc.ExecuteStrategy(It.IsAny<object[]>())).Returns(MockEnStr .Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "BuildMacroCommandStrat", (object[] args) => MockStrategyParams.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "LongTermOperationStrategy", (object[] args) => MockStrategyParams.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SetUpOperation.Moving", (object[] args) => MockStratReturnStr.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create.MacroCommand", (object[] args) =>  MockStrategyParams.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.InjectCommand", (object[] args) => MockStrategyParams.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.RepeadCommand", (object[] args) => MockStrategyParams.Object.ExecuteStrategy(args)).Execute();
    }

    [Fact]
    public void LongTermOperationStrategyTest() {
        IStrategy LO = new LOstrategy();
        string name = "Moving";
        var obj = new Mock<IUObject>();
        Assert.NotNull(LO.ExecuteStrategy(name, obj.Object));
    }
}