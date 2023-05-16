namespace BattleSpace.Lib.Test;

public class SetPropertyStrategy : IStrategy {
    ISetProperty setProperty;
    
    public SetPropertyStrategy(ISetProperty setProperty) {
        this.setProperty = setProperty;
    }

    public object ExecuteStrategy(params object[] args) {
        var uobject = (IUObject)args[0];
        var property = (IList<string>) args[1];

        return new SetProperty(setProperty, uobject, property);
    }
}
