using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib {
    public class GetDeltasStrategy : IStrategy
    {
        public object ExecuteStrategy(params object[] args)
        {
            var list_1 = IoC.Resolve<List<int>>("Collision.GetList", (IUObject) args[0]);
            var list_2 = IoC.Resolve<List<int>>("Collision.GetList", (IUObject) args[1]); 

            var list = list_1.Zip(list_2, (l1, l2) => l1 - l2);

            return list;
        }
        
    }
}
