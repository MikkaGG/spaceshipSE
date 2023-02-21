using System;
using System.Collections.Generic;

namespace BattleSpace.Lib.Test;

public class SetProperty : ICommand {
    IUObject uobject;
    IList<string> property;
    ISetProperty setProperty;

    public SetProperty(ISetProperty setProperty, IUObject uobject, IList<string> property) {
        this.uobject = uobject;
        this.property = property;
        this.setProperty = setProperty;
    }

    public void Execute() {
        setProperty.SetProperty(uobject, property);
    }
}
