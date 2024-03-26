namespace Reverso.Model;

public class HumanPlayer : Player
{
    private readonly IInputHandler _inputHandler;
    public HumanPlayer(string name, IInputHandler inputHandler) : base(name)
    {
        _inputHandler = inputHandler;
    }

    public override int MakeMoveOnField(Field GameField)
    {
        int[] coordinates = _inputHandler.GetPlayerCoords(GameField);
        return GameField.ChangeField(coordinates[0], coordinates[1], this);
    }
}

public interface IInputHandler
{
    public int[] GetPlayerCoords(Field GameField);
}