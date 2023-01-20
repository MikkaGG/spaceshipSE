namespace BattleSpace.Lib;
public class MacroCommand : ICommand {   
    private List<ICommand> list_command;
    public MacroCommand(List<ICommand> list_command) {
        this.list_command = list_command;
    }
    public void Execute() {
        foreach (var command in list_command) {
            command.Execute();
        }
    }
}
