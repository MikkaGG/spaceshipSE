using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib {
    public class StartMoveCommand : ICommand {
        IMoveCommandStartable obj;

        public StartMoveCommand(IMoveCommandStartable obj) {
            this.obj = obj;
        }

        public void Execute() {
            var uobject = obj.UObject;
            var command = obj.Command;
            var properties = obj.Properties;

            IoC.Resolve<ICommand>("Game.Comands.SetProperty", uobject, properties).Execute();
            IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<IRepeatable>("Game.Comands.Repeat", uobject, command).Repeat());
        }
    }
}
