using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib.Test;

public class SetIniPositionTests
{
    [Fact]
    public void PosTest_SetIniFuel()
    {
        Mock<ICommand> mcmd = new();
        mcmd.Setup(_m => _m.Execute()).Verifiable();

        Mock<IStrategy> mStrat = new();
        mStrat.Setup(_m => _m.ExecuteStrategy(It.IsAny<object[]>())).Returns(mcmd.Object);

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.SetIniFuel", (object[] props) => new SetIniFuelStrat().ExecuteStrategy(props)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Services.GetInitialFuel", (object[] props) => (object)10).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.UObject.Set", (object[] props) => mStrat.Object.ExecuteStrategy(props)).Execute();

        
        var poit = new FuelIterator();
        var iterStrat = new FuelIteratorGetAndMove(poit);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.IniFuelIter.Next", (object[] props) => iterStrat.ExecuteStrategy()).Execute();
        
        Mock<IUObject> patient = new();

        IoC.Resolve<ICommand>("Game.SetIniFuel", patient.Object).Execute();
        IoC.Resolve<ICommand>("Game.SetIniFuel", patient.Object).Execute();

        mcmd.VerifyAll();

        poit.Reset();
        poit.Dispose();
    }
}
