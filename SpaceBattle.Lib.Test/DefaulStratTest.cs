namespace BattleSpace.Lib.Test;

public class DefaultStratTest
{
    [Fact]
    public void PostTest_ThrowBack()
    {
        ArgumentException ae = new();

        DefaultStrat df = new();

        Assert.Throws<ArgumentException>(() => df.ExecuteStrategy(ae));
    }
}
