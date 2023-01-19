using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib;
public class CollisionCheck : ICommand {
    private readonly IUObject uobj_1, uobj_2;
    public CollisionCheck(IUObject _uobj_1, IUObject _uobj_2) {
        this.uobj_1 = _uobj_1;
        this.uobj_2 = _uobj_2;
    }
    
    public void Execute() {   
        var list = IoC.Resolve<IEnumerable<int>>("Collision.GetDeltas", uobj_1, uobj_2);

        if (IoC.Resolve<bool>("Collision.CheckWithTree", list)) {
            throw new Exception("Collision");
        }
    }
}
