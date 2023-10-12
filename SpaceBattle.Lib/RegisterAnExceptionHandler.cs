using Hwdtech;
using Hwdtech.Ioc;

namespace BattleSpace.Lib {
    public class RegisterAnExceptionHandler : ICommand{
        private IEnumerable<Type> _enumerable;
        private IHandler _handler;

        public RegisterAnExceptionHandler(IEnumerable<Type> enumerable, IHandler handler) {
            _enumerable = enumerable;
            _handler = handler;
        }

        public void Execute(){
            var tree = IoC.Resolve<IDictionary<int, IHandler>>("Game.ExceptionHandlingTree.Get");
            var hash = IoC.Resolve<int>("Game.HashCode.Get", _enumerable);
            tree.TryAdd(hash, _handler);
        }
    }
}
