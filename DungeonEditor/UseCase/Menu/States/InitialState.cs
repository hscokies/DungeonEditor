using DungeonEditor.Interfaces;
using DungeonEditor.Models;
using DungeonEditor.UseCase.Menu.Constants;

namespace DungeonEditor.UseCase.Menu.States;

public sealed class InitialState : IState
{
    private static bool IsValidFilePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path) || !Path.Exists(path))
        {
            Console.WriteLine($"cannot open '{path}': for reading: No such file or directory");
            return false;
        }

        if (!Directory.Exists(path)) return true;

        Console.WriteLine($"error reading '{path}': Is a directory");
        return false;

    }

    public Task OnStateChanged()
    {
        if (Store.TryGet<string>(StoreConstants.FilePath, out var path) &&
            IsValidFilePath(path))
        {
            return StateMachine.ChangeState(new DungeonsMenuState());
        }
        
        Console.WriteLine("Enter save data path:");
        return StateMachine.WaitForUserInput();
    }

    public async Task OnUserInput(string input)
    {
        if (!IsValidFilePath(input))
        {
            await StateMachine.WaitForUserInput();
            return;
        }
        Store.Remove(StoreConstants.FilePath);

        var saveData = new SaveFile(input);
        await saveData.ReadAsync();
        Store.Set(StoreConstants.SaveFile, saveData);

        await StateMachine.ChangeState(new DungeonsMenuState());
    }
}