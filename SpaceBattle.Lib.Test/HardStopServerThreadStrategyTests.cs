using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace BattleSpace.Lib.Test;

public class HardStopServerThreadStrategyTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public HardStopServerThreadStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadStrategyThrowsExceptionBecauseFalseKeyInExecuteStrategyCondition()
    {
        var key = 1;
        var falseKey = 2;

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.ExecuteStrategy(key);
        c.Execute();

        var hardStopStrategy = new HardStopServerThreadStrategy();

        Assert.Throws<Exception>(() =>
        {
            var hs = (ICommand)hardStopStrategy.ExecuteStrategy(falseKey);
        });

        var hs = (ICommand)hardStopStrategy.ExecuteStrategy(key);
        hs.Execute();
    }
}
