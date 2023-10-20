using Hwdtech;

namespace BattleSpace.Lib;

public class FuelIterator : IEnumerator<object>
{
    public object Current => IoC.Resolve<int>("Services.GetInitialFuel");

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool MoveNext()
    {
        return true;
    }

    public void Reset(){}
}
