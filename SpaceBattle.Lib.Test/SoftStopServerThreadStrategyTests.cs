using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace BattleSpace.Lib.Test;

public class SoftStopServerThreadCStrategyTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public SoftStopServerThreadCStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void SuccessfulSoftStopServerThreadCStrategyExecuteStrategy()
    {
        var key = 1;

        var ssFlag = false;

        var are = new AutoResetEvent(false);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.ExecuteStrategy(key);
        c.Execute();

        var softStopStrategy = new SoftStopServerThreadStrategy();
        var ss = (ICommand)softStopStrategy.ExecuteStrategy(key, () =>
        {
            ssFlag = true;
            are.Set();
        });
        ss.Execute();

        are.WaitOne();

        Assert.True(ssFlag);
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadStrategyExecuteStrategyBecauseExecuteStrategyThrowsExceptionWithUncorrectServerThreadKey()
    {
        var key = 2;
        var falseKey = 3;

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.ExecuteStrategy(key);
        c.Execute();

        var softStopStrategy = new SoftStopServerThreadStrategy();

        Assert.Throws<Exception>(() =>
        {
            var ss = (ICommand)softStopStrategy.ExecuteStrategy(falseKey);
        });

        var hardStopStrategy = new HardStopServerThreadStrategy();
        var hs = (ICommand)hardStopStrategy.ExecuteStrategy(key);
        hs.Execute();
    }
}
