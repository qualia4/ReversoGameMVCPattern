namespace Reverso.Model;

public class HumanPlayer : Player
{
    private IInputHandler _inputHandler;
    public HumanPlayer(string name) : base(name)
    {
    }

    public void SetCommandHandler(IInputHandler inputHandler)
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