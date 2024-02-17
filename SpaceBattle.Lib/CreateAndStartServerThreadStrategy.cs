using Hwdtech;
using System.Collections.Concurrent;

namespace BattleSpace.Lib;

public class CreateAndStartServerThreadStrategy : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {}; 

        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var cmd = new CreateAndStartServerThreadCommand(id);

        return new ActionCommand(() => {
            cmd.Execute();
            action();
        });
    }
}
