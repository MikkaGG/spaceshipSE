namespace BattleSpace.Lib;

public interface IMoveCommandEndable {
    IUObject UObject { 
        get; 
    }
    ICommand Cmd {
        get;
    }
}
