using System.Diagnostics;
using Hwdtech;

namespace BattleSpace.Lib;

public class SpaceBattleCMD : ICommand
{
    object scope;
    Queue<ICommand> q;

    public SpaceBattleCMD(object scope, Queue<ICommand> queue)
    {
        this.scope = scope;
        this.q = queue;
    }

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
        int quant = IoC.Resolve<int>("Game.Get.Quant");
        Stopwatch stopWatch = new();
        while (stopWatch.ElapsedMilliseconds < quant && q.Count > 0)
        {
            stopWatch.Start();
            ICommand cmd = IoC.Resolve<ICommand>("Game.Queue.Take", q);
            try
            {
                cmd.Execute();
            }
            catch (Exception e)
            {
                IoC.Resolve<ICommand>(IoC.Resolve<string>("Game.Options.GetHandler", cmd, e)).Execute();
            }
            stopWatch.Stop();
        }
    }
}
