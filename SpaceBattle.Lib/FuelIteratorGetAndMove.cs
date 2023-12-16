namespace BattleSpace.Lib;

public class FuelIteratorGetAndMove : IStrategy
{
    IEnumerator<object> poit;

    public FuelIteratorGetAndMove(IEnumerator<object> poit)
    {
        this.poit = poit;
    }

    public object ExecuteStrategy(params object[] args)
    {
        int c = (int)poit.Current;
        poit.MoveNext();
        return c;
    }
}
