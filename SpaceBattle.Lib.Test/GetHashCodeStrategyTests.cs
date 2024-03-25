using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace BattleSpace.Lib.Test;

public class GetHashCodeStrategyTests
{
    [Fact]
    public void EqualHashCodesForDifferentPermutationsOfObjects()
    {
        var a = new List<Type>() { typeof(IOException), typeof(ArgumentException), typeof(StartCommand) };
        var b = new List<Type>() { typeof(StartCommand), typeof(ArgumentException), typeof(IOException) };
        var c = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };

        var strategy = new GetHashCodeStrategy();

        Assert.Equal(strategy.ExecuteStrategy(a), strategy.ExecuteStrategy(b));
        Assert.Equal(strategy.ExecuteStrategy(c), strategy.ExecuteStrategy(b));
        Assert.Equal(strategy.ExecuteStrategy(a), strategy.ExecuteStrategy(c));
    }
    [Fact]
    public void NotEqualHashCodesForDifferentObjects()
    {
        var a = new List<Type>() { typeof(IOException), typeof(ArgumentException), typeof(StartCommand) };
        var b = new List<Type>() { typeof(StartCommand), typeof(ArgumentException) };

        var strategy = new GetHashCodeStrategy();

        Assert.NotEqual(strategy.ExecuteStrategy(a), strategy.ExecuteStrategy(b));
    }
    [Fact]
    public void EqualHashCodesForSameObjects()
    {
        var a = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };
        var b = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };
        var c = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };

        var strategy = new GetHashCodeStrategy();

        Assert.Equal(strategy.ExecuteStrategy(a), strategy.ExecuteStrategy(b));
        Assert.Equal(strategy.ExecuteStrategy(c), strategy.ExecuteStrategy(b));
        Assert.Equal(strategy.ExecuteStrategy(a), strategy.ExecuteStrategy(c));
    }
}
