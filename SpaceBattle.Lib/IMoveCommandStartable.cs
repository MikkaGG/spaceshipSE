namespace BattleSpace.Lib;
public interface IMoveCommandStartable {
    IUObject UObject { 
        get; 
    }
    ICommand Command {
        get;
    }
    IList<string> Properties {
        get;
    }
}
