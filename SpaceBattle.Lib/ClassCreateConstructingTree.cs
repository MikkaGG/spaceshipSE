using Hwdtech;

namespace SpaceBattle.lib;

public class CreateTreeCommand: ICommand{
    private string name_way;

    public CreateTreeCommand(string name_way){
        this.name_way=name_way;
    }

    public void Execute()
    {
        var vectors = File.ReadAllLines(name_way).ToList().Select(l=>l.Split(" ").Select(int.Parse).ToList()).ToList();
        var tree = IoC.Resolve<IDictionary<int,object>>("Game.getCollisionTree");
        foreach(List<int> vector in vectors){
            var iter = tree;
            foreach(int value in vector){
                iter.TryAdd(value, new Dictionary<int,object>());
                iter = (Dictionary<int, object>) iter[value];
            }
        }

    }
}
