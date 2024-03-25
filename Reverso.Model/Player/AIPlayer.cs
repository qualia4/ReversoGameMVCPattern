namespace Reverso.Model;

public class AIPlayer: Player
{
    Random rand = new Random();

    public AIPlayer(string name) : base(name)
    {
    }

    public override int MakeMoveOnField(Field GameField)
    {
        int[] coordinates = GenerateRandomMove(GameField);
        return GameField.ChangeField(coordinates[0], coordinates[1], this);
    }

    private int[] GenerateRandomMove(Field GameField)
    {
        while (true)
        {
            int x = rand.Next(0, 8);
            int y = rand.Next(0, 8);
            if (GameField.GetValid(x, y))
            {
                int[] coords = new int[2] {x, y};
                int delay = rand.Next(1, 3);
                Thread.Sleep((int)TimeSpan.FromSeconds(delay).TotalMilliseconds);
                return coords;
            }
        }

    }
}