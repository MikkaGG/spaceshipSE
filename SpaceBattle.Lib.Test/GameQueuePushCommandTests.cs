using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace BattleSpace.Lib.Test;

public class GameQueuePushCommandTests
{
    [Fact]
    public void SuccessfulGameQueuePushCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();

        var getGameQueueByIDStrategy = new Mock<IStrategy>();
        getGameQueueByIDStrategy.Setup(s => s.ExecuteStrategy(It.IsAny<int>())).Returns(queue).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetGameQueueByID", (object[] args) => getGameQueueByIDStrategy.Object.ExecuteStrategy(args)).Execute();

        var gameQueuePushCommand = new GameQueuePushCommand(1, cmd.Object);

        gameQueuePushCommand.Execute();

        Assert.True(queue.Count == 1);
        getGameQueueByIDStrategy.VerifyAll();
    }
}
