namespace BattleSpace.Lib;

public class PosIterGetAndMove : IStrategy
{
    IEnumerator<object> poit;

    public PosIterGetAndMove(IEnumerator<object> poit)
    {
        this.poit = poit;
    }

    public object ExecuteStrategy(params object[] args)
    {
        Vector c = (Vector)poit.Current;
        bool m = poit.MoveNext();
        if (!m) {poit.Reset();}
        return c;
    }
}
