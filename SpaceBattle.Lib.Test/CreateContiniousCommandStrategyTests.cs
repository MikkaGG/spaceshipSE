using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace BattleSpace.Lib.Test;

public class CreateContiniousCommandStrategyTests
{
    [Fact]
    public void SuccesfulCreateContiniousCommandRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<ICommand>();

        var mockIUObj = new Mock<IUObject>();

        var mockStrategyReturnsMacroCommand = new Mock<IStrategy>();
        mockStrategyReturnsMacroCommand.Setup(m => m.ExecuteStrategy(It.IsAny<IEnumerable<ICommand>>())).Returns(mockCommand.Object).Verifiable();
    
        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsMacroCommand.Setup(m => m.ExecuteStrategy(It.IsAny<IUObject>())).Returns(mockCommand.Object).Verifiable();

        var mockIEnumString = new Mock<IEnumerable<string>>();

        var mockStrategyReturnsIEnumString = new Mock<IStrategy>();
        mockStrategyReturnsIEnumString.Setup(m => m.ExecuteStrategy()).Returns(mockIEnumString.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SetupOperation.MyName", (object[] args) => mockStrategyReturnsIEnumString.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateMacroCommand", (object[] args) => mockStrategyReturnsMacroCommand.Object.ExecuteStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "MyName", (object[] args) => mockStrategyReturnsCommand.Object.ExecuteStrategy(args)).Execute();

        var strategy = new CreatePartCommandStrategy();
        
        strategy.ExecuteStrategy("MyName", mockIUObj.Object);
        
        mockStrategyReturnsCommand.VerifyAll();
        mockStrategyReturnsIEnumString.VerifyAll();
    }
}
