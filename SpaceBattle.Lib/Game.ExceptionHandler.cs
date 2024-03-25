using Hwdtech;

namespace BattleSpace.Lib;

public class FindExceptionHandlerStrategy : IStrategy
{
    public object ExecuteStrategy(params object[] args)
    {
        var list = (IEnumerable<Type>)args[0];

        var tree = IoC.Resolve<IDictionary<int, IHandler>>("Game.GetExceptionTree");

        return tree.TryGetValue(IoC.Resolve<int>("Game.GetHashCode", list), out IHandler? value) ? value : tree[0];
    }
}
