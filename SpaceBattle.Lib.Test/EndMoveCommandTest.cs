using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace BattleSpace.Lib.Test;

public class EndMoveCommandTests {
    public EndMoveCommandTests() {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var deleteProperty = new Mock<IPropertyDelete>();
        var someObj = new Mock<IUObject>();
        deleteProperty.Setup(p => p.DeleteProperty(someObj.Object, It.IsAny<string>()));
        var deletePropertyStrategy = new DeletePropertyStrategy(deleteProperty.Object);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.DeleteObjectPropertyCommand", (object[] args) => deletePropertyStrategy.ExecuteStrategy(args)).Execute();
        
        var injectStrategy = new InjectCommandStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.EmptyCommand", (object[] args) => injectStrategy.ExecuteStrategy(args)).Execute();
    
        var queueStrategy = new Mock<IStrategy>();
        queueStrategy.Setup(p => p.ExecuteStrategy(It.IsAny<object[]>()));
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queueStrategy.Object.ExecuteStrategy(args)).Execute();
    }

    [Fact]
    public void EndMoveCommandPos() {
        var moveendable = new Mock<IMoveCommandEndable>();
     
        moveendable.SetupGet<IUObject>(m => m.UObject).Returns(new Mock<IUObject>().Object);
        moveendable.SetupGet<ICommand>(m => m.Cmd).Returns(new Mock<ICommand>().Object);
        moveendable.SetupGet<IList<string>>(m => m.Properties).Returns(new Mock<IList<string>>().Object);

        var endMoveCommand = new EndMoveCommand(moveendable.Object);
        endMoveCommand.Execute();
        moveendable.VerifyAll();
    }

    [Fact]
    public void UnableGetObjectTest() {
        var moveendable = new Mock<IMoveCommandEndable>();
     
        moveendable.SetupGet<IUObject>(m => m.UObject).Throws<Exception>();
        moveendable.SetupGet<ICommand>(m => m.Cmd).Returns(new Mock<ICommand>().Object);
        moveendable.SetupGet<IList<string>>(m => m.Properties).Returns(new Mock<IList<string>>().Object);

        var endMoveCommand = new EndMoveCommand(moveendable.Object);
        Assert.Throws<Exception>(() => endMoveCommand.Execute());
    }
    
    [Fact]
    public void UnableGetCommandTest() {
        var moveendable = new Mock<IMoveCommandEndable>();
     
        moveendable.SetupGet<IUObject>(m => m.UObject).Returns(new Mock<IUObject>().Object);
        moveendable.SetupGet<ICommand>(m => m.Cmd).Throws<Exception>();
        moveendable.SetupGet<IList<string>>(m => m.Properties).Returns(new Mock<IList<string>>().Object);

        var endMoveCommand = new EndMoveCommand(moveendable.Object);
        Assert.Throws<Exception>(() => endMoveCommand.Execute());
    }
    
    [Fact]
    public void UnableGetPropertiesTest() {
        var moveendable = new Mock<IMoveCommandEndable>();
     
        moveendable.SetupGet<IUObject>(m => m.UObject).Returns(new Mock<IUObject>().Object);
        moveendable.SetupGet<ICommand>(m => m.Cmd).Returns(new Mock<ICommand>().Object);
        moveendable.SetupGet<IList<string>>(m => m.Properties).Throws<Exception>();

        var endMoveCommand = new EndMoveCommand(moveendable.Object);
        Assert.Throws<Exception>(() => endMoveCommand.Execute());
    }
}

