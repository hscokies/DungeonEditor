namespace DungeonEditor.Interfaces;

public interface IState
{
    public Task OnStateChanged();
    public Task OnUserInput(string input);
}