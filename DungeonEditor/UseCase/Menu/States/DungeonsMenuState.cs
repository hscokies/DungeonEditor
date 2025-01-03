using DungeonEditor.Helpers;
using DungeonEditor.Interfaces;
using DungeonEditor.Models;
using DungeonEditor.UseCase.Menu.Constants;

namespace DungeonEditor.UseCase.Menu.States;

public class DungeonsMenuState : IState
{
    private Dungeon[] _dungeons { get; set; }
    public Task OnStateChanged()
    {
        Guard.IsTrue(Store.TryGet<SaveFile>(StoreConstants.SaveFile, out var save),"cannot access save data: Object not present in data store");
        _dungeons = save.Dungeons.ToArray();
        
        for (var i = 0; i < _dungeons.Length; i++)
        {
            Console.WriteLine($"[{i+1}] {_dungeons[i].DungeonId:X}");
        }
        Console.WriteLine("[0] Back");
        return StateMachine.WaitForUserInput();
    }

    public Task OnUserInput(string input)
    {
        if (!byte.TryParse(input, out var index))
        {
            return OnUnknownInput(input);
        }

        return index switch
        {
            0 => StateMachine.Back(),
            _ when _dungeons.Length >= index => StateMachine.ChangeState(new DungeonViewState(_dungeons[index - 1])),
            _ => OnUnknownInput(input)
        };
    }

    private Task OnUnknownInput(string input)
    {
        Console.WriteLine($"not an editor command: {input}");
        return StateMachine.WaitForUserInput();
    }
}