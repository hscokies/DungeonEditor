using DungeonEditor.Interfaces;
using DungeonEditor.Models;

namespace DungeonEditor.UseCase.Menu.States;

public class DungeonViewState(Dungeon dungeon) : IState
{
    public Task OnStateChanged()
    {
        Console.WriteLine(dungeon);
        return StateMachine.WaitForUserInput();
    }

    public Task OnUserInput(string input)
    {
        throw new NotImplementedException();
    }
}