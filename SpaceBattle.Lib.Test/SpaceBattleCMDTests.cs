using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test;

public class SpaceBattleCMDTests
{
    [Fact]
    public void PosTest_SpaceBattleCMDExec()
    {
        Queue<ICommand> queue = new();

        bool wasCalled = false;
        Mock<ICommand> mcmd = new();
        mcmd.Setup(_m => _m.Execute()).Callback(() => wasCalled = true);

        ManualResetEvent mre = new(false);
        ICommand longCmd = new ActionCommand((arg) => mre.WaitOne(500));

        bool wasCalled1 = false;
        Mock<ICommand> mcmd1 = new();
        mcmd1.Setup(_m => _m.Execute()).Callback(() => wasCalled1 = true);

        Mock<IStrategy> quant400 = new();
        quant400.Setup(_q => _q.ExecuteStrategy()).Returns(400);

        new InitScopeBasedIoCImplementationCommand().Execute();

        object parentScope = IoC.Resolve<object>("Scopes.Root");
        object scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Get.Quant", (object[] props) => quant400.Object.ExecuteStrategy()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Take", (object[] props) => new TakeStrat().ExecuteStrategy(props)).Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", parentScope).Execute();

        ICommand game = new SpaceBattleCMD(scope, queue);

        queue.Enqueue(mcmd.Object);
        queue.Enqueue(longCmd);
        queue.Enqueue(mcmd1.Object);

        game.Execute();

        Assert.True(wasCalled);
        Assert.False(wasCalled1);
        Assert.NotEmpty(queue);
    }
    [Fact]
    public void NegTest_SpaceBattleCMDExecThrowsException()
    {
        Queue<ICommand> queue = new();

        Mock<ICommand> mcmd = new();
        mcmd.Setup(_m => _m.Execute()).Throws(It.IsAny<Exception>());

        Mock<IStrategy> quant400 = new();
        quant400.Setup(_q => _q.ExecuteStrategy()).Returns(400);

        Mock<IStrategy> handlerGetter = new();
        handlerGetter.Setup(_h => _h.ExecuteStrategy(It.IsAny<object[]>())).Returns("Game.Handler.Foo");

        Mock<ICommand> handCmd = new();
        handCmd.Setup(_h => _h.Execute()).Verifiable();

        Mock<IStrategy> fooHandler = new();
        fooHandler.Setup(_h => _h.ExecuteStrategy(It.IsAny<object[]>())).Returns(handCmd.Object);

        new InitScopeBasedIoCImplementationCommand().Execute();

        object parentScope = IoC.Resolve<object>("Scopes.Root");
        object scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Get.Quant", (object[] props) => quant400.Object.ExecuteStrategy()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Take", (object[] props) => new TakeStrat().ExecuteStrategy(props)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Options.GetHandler", (object[] props) => handlerGetter.Object.ExecuteStrategy(props)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Handler.Foo", (object[] props) => fooHandler.Object.ExecuteStrategy(props)).Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", parentScope).Execute();

        ICommand game = new SpaceBattleCMD(scope, queue);

        queue.Enqueue(mcmd.Object);

        game.Execute();

        handCmd.VerifyAll();
    }
}
