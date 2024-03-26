namespace Reverso.Model;

public class AIPlayer: Player
{
    private readonly Random rand = new Random();
    private readonly bool makeDelay;

    public AIPlayer(string name, bool makeDelay = true) : base(name)
    {
        this.makeDelay = makeDelay;
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
                int[] coords = new int[] {x, y};
                if (makeDelay)
                {
                    int delay = rand.Next(1, 3);
                    Thread.Sleep((int)TimeSpan.FromSeconds(delay).TotalMilliseconds);
                }
                return coords;
            }
        }

    }
}