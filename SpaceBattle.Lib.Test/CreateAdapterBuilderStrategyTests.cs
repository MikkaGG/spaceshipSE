using Moq;

namespace BattleSpace.Lib.Test;


public class CreateAdapterBuilderStrategyTests
{
    [Fact]
    public void SuccessfulCreateAdapterBuilderStrategyRunStrategy()
    {
        var strategy = new CreateAdapterBuilderStrategy();

        var result = (IBuilder)strategy.ExecuteStrategy(typeof(IUObject), typeof(IMovable));

        Assert.IsType<AdapterBuilder>(result);
        Assert.NotNull(result);
    }
}
