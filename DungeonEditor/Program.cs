using DungeonEditor;
using DungeonEditor.Enumerations;
using DungeonEditor.Helpers;
using DungeonEditor.Models;
using DungeonEditor.UseCase.Menu;
using DungeonEditor.UseCase.Menu.States;

internal class Program
{
    public static async Task Main(string[] args)
    {
        StateMachine.Build(args);
        await StateMachine.ChangeState(new InitialState());
    }
}

//
//
//
//
// var savefile = new SaveFile(path);
// await savefile.ReadAsync(default);
//
// foreach (var dungeon in savefile.Dungeons)
// {
//     Console.WriteLine(dungeon);
//     Console.WriteLine();
// }
    
    //var gempool = GempoolCalculator.GetGemPool(categories);
    // Console.WriteLine("\nPrimaries:");
    // foreach (var (gem, chance) in gempool.Primaries.OrderByDescending(x => x.Value))
    // {
    //     Console.WriteLine($"{gem}: {chance:F3}");
    // }
    // Console.WriteLine("\nSecondaries:");
    // foreach (var (gem, chance) in gempool.Secondaries.OrderByDescending(x => x.Value))
    // {
    //     Console.WriteLine($"{gem}: {chance:F3}");
    // }
