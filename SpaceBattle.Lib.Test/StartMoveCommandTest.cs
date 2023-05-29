using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace BattleSpace.Lib.Test;

public class StartMoveCommandTest {
    public StartMoveCommandTest() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var setProperty = new Mock<ISetProperty>();
        var uObject = new Mock<IUObject>();
        setProperty.Setup(m => m.SetProperty(uObject.Object, It.IsAny<IList<string>>()));

        var setPropertyStrategy = new SetPropertyStrategy(setProperty.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Comands.SetProperty", (object[] args) => (setPropertyStrategy.ExecuteStrategy(args))).Execute();

        var repeatStrategy = new RepeatCommandStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Comands.Repeat", (object[] args) => (repeatStrategy.ExecuteStrategy(args))).Execute();

        var queue = new Mock<IStrategy>();
        queue.Setup(p => p.ExecuteStrategy(It.IsAny<object[]>()));
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queue.Object.ExecuteStrategy(args)).Execute();
    }

    [Fact]
    public void StartableCommandTest() {
        var moveStart = new Mock<IMoveCommandStartable>();
        moveStart.SetupGet<ICommand>(m => m.Command).Returns(new Mock<ICommand>().Object);
        moveStart.SetupGet<IUObject>(m => m.UObject).Returns(new Mock<IUObject>().Object);
        moveStart.SetupGet<IList<string>>(m => m.Properties).Returns(new Mock<IList<string>>().Object);

        var startCommand = new StartMoveCommand(moveStart.Object);
        startCommand.Execute();

        moveStart.VerifyAll();
    }

    [Fact]
    public void UnreadableCommandTest() {
        var moveStart = new Mock<IMoveCommandStartable>();
        moveStart.SetupGet<ICommand>(m => m.Command).Throws<Exception>();
        moveStart.SetupGet<IUObject>(m => m.UObject).Returns(new Mock<IUObject>().Object);
        moveStart.SetupGet<IList<string>>(m => m.Properties).Returns(new Mock<IList<string>>().Object);

        var startCommand = new StartMoveCommand(moveStart.Object);

        Assert.Throws<Exception>(() => startCommand.Execute());
    }

    [Fact]
    public void UnreadableObjectTest() {
        var moveStart = new Mock<IMoveCommandStartable>();
        moveStart.SetupGet<ICommand>(m => m.Command).Returns(new Mock<ICommand>().Object);
        moveStart.SetupGet<IUObject>(m => m.UObject).Throws<Exception>();
        moveStart.SetupGet<IList<string>>(m => m.Properties).Returns(new Mock<IList<string>>().Object);

        var startCommand = new StartMoveCommand(moveStart.Object);

        Assert.Throws<Exception>(() => startCommand.Execute());
    }

    [Fact]
    public void UnreadablePropertiesTest() {
        var moveStart = new Mock<IMoveCommandStartable>();
        moveStart.SetupGet<ICommand>(m => m.Command).Returns(new Mock<ICommand>().Object);
        moveStart.SetupGet<IUObject>(m => m.UObject).Returns(new Mock<IUObject>().Object);
        moveStart.SetupGet<IList<string>>(m => m.Properties).Throws<Exception>();

        var startCommand = new StartMoveCommand(moveStart.Object);

        Assert.Throws<Exception>(() => startCommand.Execute());
    }
}
