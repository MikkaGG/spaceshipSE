using Moq;

namespace BattleSpace.Lib.Test;

public class GameUObjectSetPropertyStrategyTests
{
    [Fact]
    public void SuccessfulGameUObjectSetPropertyStrategyRunStrategy()
    {
        var gameUObjectSetPropertyStrategy = new GameUObjectSetPropertyStrategy();

        Assert.NotNull(gameUObjectSetPropertyStrategy.ExecuteStrategy(new Mock<IUObject>().Object, "ldjkgkdg", 1));
    }
}
