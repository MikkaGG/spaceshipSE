using Moq;

namespace BattleSpace.Lib.Test;

public class GameQueuePushStrategyTests
{
    [Fact]
    public void SuccessfulGameQueuePushStrategyRunStrategy()
    {
        var gameQueuePushStrategy = new GameQueuePushStrategy();

        Assert.NotNull(gameQueuePushStrategy.ExecuteStrategy(1, new Mock<ICommand>().Object));
    }
}
