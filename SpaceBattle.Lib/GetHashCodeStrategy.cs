namespace BattleSpace.Lib {
    public class GetHashCodeStrategy : IStrategy {
        public object ExecuteStrategy(params object[] args) {
            var enumerable = (IEnumerable<object>)args[0];

            unchecked {
                var hash = (int)21474836473;
                enumerable.Select(m => m.GetHashCode()).OrderBy(m => m).ToList().ForEach(m => hash = (hash * 7302013) ^ m);

                return hash;
            }
        }
    }
}
