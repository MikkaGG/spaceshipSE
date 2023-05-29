using SpaceBattle.Lib.EndPoint;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test;

public class EndPointTests
{
    [Fact]
    public void PosTest_SendMessage()
    {
        Mock<ICommand> mcmd = new();
        mcmd.Setup(_m => _m.Execute()).Verifiable();

        Mock<IStrategy> mSend = new();

        mSend.Setup(_m => _m.ExecuteStrategy(It.IsAny<object[]>())).Returns(mcmd.Object);

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Send Message", (object[] args) => mSend.Object.ExecuteStrategy(args)).Execute();

        Order ord = new();
        ord.type = "start move";
        ord.gameId = 1;
        ord.gameItemId = "565262";
        ord.others = new();
        ord.others.param = "init vel";
        ord.others.val = 5;

        var endp = new SpaceBattle.Lib.EndPoint.EndPoint().OrderBodyEcho(ord);

        mcmd.VerifyAll();
    }
}
