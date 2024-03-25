using System.Reflection;

namespace BattleSpace.Lib;


public interface IBuilder
{
    public String Build();
    public void AddProperty(object property);
}
