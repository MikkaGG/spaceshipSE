namespace BattleSpace.Lib.Test;

public class DeletePropertyStrategy : IStrategy {
    IPropertyDelete _propertyDelete;

    public DeletePropertyStrategy(IPropertyDelete propertyDelete) {
        _propertyDelete = propertyDelete;
    }

    public object ExecuteStrategy(params object[] args) {
        var obj = (IUObject)args[0];
        var properties = (IList<string>)args[1];

        return new DeleteProperty(_propertyDelete, obj, properties);
    }
}
