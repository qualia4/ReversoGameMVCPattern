namespace Reverso.Model.Abstractions;

public interface IInputHandler
{
    public int[] GetPlayerCoords(IGameField gameReversoField);
}