using DungeonEditor.Interfaces;
using DungeonEditor.UseCase.Menu.Constants;
using DungeonEditor.UseCase.Menu.States;

namespace DungeonEditor.UseCase.Menu;

public static class StateMachine
{
    private static Stack<IState> States = new();
    public static IState State => States.Peek(); 

    public static void Build(string[] args)
    {
        if (args.Length > 0)
        {
            Store.TrySet(StoreConstants.FilePath, args[0]);
        }
    }

    public static Task ChangeState(IState state)
    {
        States.Push(state);
        Console.Clear();
        return state.OnStateChanged();
    }

    public static Task Back()
    {
        Console.Clear();
        if (States.TryPop(out _))
        {
            return State.OnStateChanged();
        }

        throw new InvalidOperationException("failed to return to last state: State stack is empty");
    }

    public static Task WaitForUserInput()
    {
        Console.Write(">");
        var input = Console.ReadLine()!;
        return State.OnUserInput(input);
    }
}