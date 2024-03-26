using Reverso.Model.Abstractions;

namespace Reverso.Controller;

public class MoveInputHandler: IInputHandler
{
    public int[] GetPlayerCoords(IGameField gameField)
    {
        string command = Console.ReadLine().ToLower();
        var splitCommand = command.Split(Array.Empty<char>());
        if (splitCommand[0] == "move")
        {
            int x = int.Parse(splitCommand[1]);
            int y = int.Parse(splitCommand[2]);
            if(gameField.IsInBounds(x, y) && gameField.IsValidCell(x, y))
            {
                int[] coords = new int[] {x, y};
                return coords;
            }
        }
        throw new Exception(command);
    }
}