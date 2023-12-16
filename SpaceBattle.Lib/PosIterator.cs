using Hwdtech;

namespace BattleSpace.Lib;

public class PosIterator : IEnumerator<object>
{
    List<int> teams;
    int innerSpace;
    int outerSpace;
    int counter = 1;
    int teamSize;
    int currentTeam = 0;
    Vector startingPoint;
    public PosIterator(List<int> teams, int innerSpace, int outerSpace)
    {
        this.teams = teams;
        this.innerSpace = innerSpace;
        this.outerSpace = outerSpace;
        this.teamSize = teams[0];
        this.startingPoint = IoC.Resolve<Vector>("Services.GetStartingPoint");
    }

    public object Current
    {
        get
        {
            Vector buf = startingPoint + new Vector(0, innerSpace * counter);
            counter++;
            return buf;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool MoveNext()
    {
        if (counter <= teamSize)
        {
            return true;
        }
        else 
        {
            currentTeam++;
            if (currentTeam < teams.Count) 
            {
                startingPoint += new Vector(outerSpace, 0);
                counter = 1;
                teamSize = teams[currentTeam];
                return true;
            }
        }
        return false;
    }

    public void Reset()
    {
        startingPoint = IoC.Resolve<Vector>("Services.GetStartingPoint");
        currentTeam = 0;
        counter = 1;
    }
}
