namespace BattleSpace.Lib.Test;
using Moq;
public class FindExcHandlerWithTreeStrategyTests
{
    public FindExcHandlerWithTreeStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void ReturnKeyValueHandlerTreeDictTest()
    {
        // Arrange
        var Handler = new Mock<IStrategy>().Object;

        var ExceptionDict = new Mock<IReadOnlyDictionary<Int32, IStrategy>>();

        var HandlerTreeDict = new Mock<IReadOnlyDictionary<Int32, IReadOnlyDictionary<Int32, IStrategy>>>();
        HandlerTreeDict.Setup(htd => htd[It.IsAny<Int32>()].TryGetValue(It.IsAny<Int32>(), out Handler)).Returns(true);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetSubTree", (object[] args) => ExceptionDict.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetTree", (object[] args) => HandlerTreeDict.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetEmptyStrategy", (object[] args) => Handler).Execute(); 

        var FindExcHandlerStrategy = new FindHandlerWithTreeStrategy();

        // Act
        // Assert
        Assert.NotNull(FindExcHandlerStrategy.ExecuteStrategy(new object[] {new Mock<ICommand>().Object, new Mock<Exception>().Object}));
    }

    [Fact]
    public void ReturnKeyValueSubTreeDictTest()
    {
        // Arrange
        var Handler = new Mock<IStrategy>().Object;

        var ExceptionDict = new Mock<IReadOnlyDictionary<Int32, IStrategy>>();
        ExceptionDict.Setup(htd => htd.TryGetValue(It.IsAny<Int32>(), out Handler)).Returns(true);

        var HandlerTreeDict = new Mock<IReadOnlyDictionary<Int32, IReadOnlyDictionary<Int32, IStrategy>>>();
        HandlerTreeDict.Setup(htd => htd[It.IsAny<Int32>()].TryGetValue(It.IsAny<Int32>(), out Handler)).Returns(false);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetSubTree", (object[] args) => ExceptionDict.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetTree", (object[] args) => HandlerTreeDict.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetEmptyStrategy", (object[] args) => Handler).Execute(); 

        var FindExcHandlerStrategy = new FindHandlerWithTreeStrategy();

        // Act
        // Assert
        Assert.NotNull(FindExcHandlerStrategy.ExecuteStrategy(new object[] {new Mock<ICommand>().Object, new Mock<Exception>().Object}));
    }

    [Fact]
    public void ExceptionHandlerWasntFoundTest()
    {
        // Arrange
        var Handler = new Mock<IStrategy>().Object;

        var SubDict = new Mock<IReadOnlyDictionary<Int32, IStrategy>>();
        SubDict.Setup(htd => htd.TryGetValue(It.IsAny<Int32>(), out Handler)).Returns(false);

        var HandlerTreeDict = new Mock<IReadOnlyDictionary<Int32, IReadOnlyDictionary<Int32, IStrategy>>>();
        HandlerTreeDict.Setup(htd => htd[It.IsAny<Int32>()].TryGetValue(It.IsAny<Int32>(), out Handler)).Returns(false);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetSubTree", (object[] args) => SubDict.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetTree", (object[] args) => HandlerTreeDict.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Exception.GetEmptyStrategy", (object[] args) => Handler).Execute(); 

        var FindExcHandlerStrategy = new FindHandlerWithTreeStrategy();

        // Act
        // Assert
        Assert.NotNull(FindExcHandlerStrategy.ExecuteStrategy(new object[] {new Mock<ICommand>().Object, new Mock<Exception>().Object}));
    }
}
