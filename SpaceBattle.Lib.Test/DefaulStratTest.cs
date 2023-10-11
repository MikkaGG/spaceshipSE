// using FluentAssertions;

namespace BattleSpace.Lib.Test;

public class DefaultStratTest
{
    [Fact]
    public void PostTest_ThrowBack()
    {
        ArgumentException ae = new();

        DefaultStrat df = new();

        // var act = () => df.ExecuteStrategy(ae);

        Assert.Throws<ArgumentException>(() => df.ExecuteStrategy(ae));

        // act.Should().Throw<ArgumentException>();
    }
}
