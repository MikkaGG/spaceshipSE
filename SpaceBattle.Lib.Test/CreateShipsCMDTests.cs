using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test;

public class CreateShipsCMDTests
{
    public CreateShipsCMDTests()
    {
        Mock<IUObject> uobj = new();

        Mock<IStrategy> createShipSt = new();
        createShipSt.Setup(_c => _c.ExecuteStrategy()).Returns(uobj.Object);

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Ship.Create", (object[] props) => createShipSt.Object.ExecuteStrategy()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.NumOfAllShips", (object[] props) => (object)6).Execute();
    }
    [Fact]
    public void PosTest_CreateShips()
    {
        Dictionary<string, IUObject> ships = new();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Get.UObjects", (object[] props) => (object)ships).Execute();

        var act = new CreateShipsCMD();

        act.Execute();

        Assert.True(ships.ToList().Count == 6);
    }
}
