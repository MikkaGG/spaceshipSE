using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace BattleSpace.Lib.Test;

public class SendCommandTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public SendCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void UnsuccessfulSendCommandExecuteThrowsException()
    {
        var key = 1;
        var falseKey = 2;

        var are = new AutoResetEvent(true);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.ExecuteStrategy(key);
        c.Execute();

        var sendStrategy = new SendCommandStrategy();
        var c1 = (ICommand)sendStrategy.ExecuteStrategy(falseKey, new ActionCommand(() =>
        {
            are.Set();
        }));

        Assert.Throws<Exception>(() =>
        {
            c1.Execute();
            are.WaitOne();
        });

        var hardStopStrategy = new HardStopServerThreadStrategy();
        var hs = (ICommand)hardStopStrategy.ExecuteStrategy(key);
        hs.Execute();
    }
}
