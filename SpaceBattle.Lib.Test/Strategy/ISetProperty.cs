using System.Collections.Generic;

namespace BattleSpace.Lib.Test;

public interface ISetProperty {
    public void SetProperty(IUObject uobject, IList<string> property);
}
