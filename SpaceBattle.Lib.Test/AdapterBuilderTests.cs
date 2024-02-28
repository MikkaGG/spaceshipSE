namespace BattleSpace.Lib.Test;


public class AdapterBuilderTests
{
    [Fact]
    public void SuccessfulAdapterBuilderBuild()
    {
        var adaptableType = typeof(IUObject);
        var adaptiveType = typeof(IMovable);

        var adapterBuilder = new AdapterBuilder(adaptableType, adaptiveType);
        adaptiveType.GetProperties().ToList().ForEach(property => adapterBuilder.AddProperty(property));

        var result = adapterBuilder.Build();

        var adapter = @"public class IMovableAdapter : IMovable
    {
        private IUObject obj;

        public IMovableAdapter(IUObject obj) => this.obj = obj;

        public Vector Position
        {
            get => IoC.Resolve<Vector>(""GetPosition"", obj);
            set => IoC.Resolve<ICommand>(""SetPosition"", obj, value).Execute();
        }

        public Vector Velocity
        {
            get => IoC.Resolve<Vector>(""GetVelocity"", obj);
            
        }
    }";

        Assert.Equal(adapter, result);
    }
}
