using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace BattleSpace.Lib.Test;

public class GetPropertyStrategyTests
{
    [Fact]
    public void SuccesfulGetPropertyStrategy()
    {
        var obj = new Mock<IUObject>();
        obj.Setup(o => o.getProperty("Speed")).Returns(new Vector(1, 1));

        var strategy = new GetPropertyStrategy();

        Assert.NotNull(strategy.ExecuteStrategy(obj.Object, "Speed"));
    }
}
