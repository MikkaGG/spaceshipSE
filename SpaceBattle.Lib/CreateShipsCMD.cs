using Hwdtech;

namespace BattleSpace.Lib;

public class CreateShipsCMD : ICommand
{
    public void Execute()
    {
        var allShips = IoC.Resolve<int>("Game.NumOfAllShips");
        var ships = IoC.Resolve<Dictionary<string, IUObject>>("Game.Get.UObjects");
        for (int i = 0; i < allShips; i++)
        {
            string id = Guid.NewGuid().ToString();
            ships[id] = IoC.Resolve<IUObject>("Game.Ship.Create");
        }
    }
}
