namespace BattleSpace.Lib;

public interface IMoveCommandEndable {
    IUObject UObject { 
        get; 
    }
    Queue<ICommand> Queue { 
        get; 
    }
}
