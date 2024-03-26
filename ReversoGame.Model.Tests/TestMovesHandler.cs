namespace ReversoGame.Model.Tests;
using Reverso.Model.Abstractions;

public class TestMovesHandler : IInputHandler
{
    private int moveIndex;
    private readonly int[,] moves =  { { 5, 4 }, { 3, 5 }, { 2, 4 }, { 5, 5 }, {4, 6}, {5, 3}, {6, 4}, {4, 5}, {4, 2} };

    public TestMovesHandler()
    {
        moveIndex = 0;
    }
    public int[] GetPlayerCoords(IGameField gameField)
    {
        int[] coords = new int[] {moves[moveIndex, 0], moves[moveIndex, 1]};
        moveIndex++;
        return coords;
    }

    public int GetMovesLength()
    {
        return moves.GetLength(0);
    }
}