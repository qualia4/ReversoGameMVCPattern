namespace Reverso.Model;

public class HumanPlayer : Player
{
    private ICommandHandler CommandHandler;
    public HumanPlayer(string name) : base(name)
    {
    }

    public void SetCommandHandler(ICommandHandler commandHandler)
    {
        CommandHandler = commandHandler;
    }

    public override int MakeMoveOnField(Field GameField)
    {
        int[] coordinates = CommandHandler.GetPlayerCoords(GameField);
        return GameField.ChangeField(coordinates[0], coordinates[1], this);
    }
}

public interface ICommandHandler
{
    public int[] GetPlayerCoords(Field GameField);
}