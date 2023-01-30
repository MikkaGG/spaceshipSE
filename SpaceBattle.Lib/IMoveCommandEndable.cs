using System.Collections.Generic;

namespace BattleSpace.Lib;

public interface IMoveCommandEndable {
    IUObject UObject { 
        get; 
    }
    ICommand Cmd {
        get;
    }
    IList<string> Properties {
        get;
    }
}
