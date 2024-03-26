namespace ReversoGame.Model.Tests;
using Reverso.Model.Game;
using Reverso.Model.Players;

public class ReversoModelTests
{
    [Test]
    public void MakeMove_BeforeStartGame_DoesThrowException()
    {
        var game = new ReversoGame();
        try
        {
            game.MakeMove();
            Assert.Fail();
        }
        catch
        {
            Assert.Pass();
        }
    }

    [Test]
    public void MakeMove_WhenHumanPlayer_DoesMakeMove()
    {
        var game = new ReversoGame();
        TestMovesHandler movesHandler = new TestMovesHandler();
        HumanPlayer firstPlayer = new HumanPlayer("A", movesHandler);
        AIPlayer secondPlayer = new AIPlayer("B");
        game.StartGame(firstPlayer, secondPlayer);
        var startingPoints = firstPlayer.GetPoints();

        game.MakeMove();

        Assert.That(firstPlayer.GetPoints(), Is.Not.EqualTo(startingPoints));
    }

    [Test]
    public void MakeMove_WhenBot_DoesMakeMove()
    {
        var game = new ReversoGame();
        AIPlayer firstPlayer = new AIPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        game.StartGame(firstPlayer, secondPlayer);
        var startingPoints = firstPlayer.GetPoints();

        game.MakeMove();

        Assert.That(firstPlayer.GetPoints(), Is.Not.EqualTo(startingPoints));
    }

    [Test]
    public void MakeMoves_FirstPlayerWins_WithThirteenPoints()
    {
        var game = new ReversoGame();
        TestMovesHandler movesHandler = new TestMovesHandler();
        HumanPlayer firstPlayer = new HumanPlayer("A", movesHandler);
        HumanPlayer secondPlayer = new HumanPlayer("B", movesHandler);
        game.StartGame(firstPlayer, secondPlayer);
        int firstPlayerExpectedScore = 13;
        int secondPlayerExpectedScore = 0;
        //Moves that should result in game ending with A winning
        for (int i = 0; i < movesHandler.GetMovesLength(); i++)
        {
            game.MakeMove();
        }

        Assert.That(firstPlayer.GetName(), Is.EqualTo(game.GetWinner()?.GetName()));
        Assert.That(firstPlayerExpectedScore, Is.EqualTo(firstPlayer.GetPoints()));
        Assert.That(secondPlayerExpectedScore, Is.EqualTo(secondPlayer.GetPoints()));
    }

    [Test]
    public void StartGame_InvokesEvent()
    {
        var game = new ReversoGameWithEvents();
        AIPlayer firstPlayer = new AIPlayer("A", false);
        AIPlayer secondPlayer = new AIPlayer("B", false);
        bool gameStartedEventRaised = false;
        game.GameStarted += () => gameStartedEventRaised = true;

        game.StartGame(firstPlayer, secondPlayer);

        Assert.IsTrue(gameStartedEventRaised);
    }


    [Test]
    public void MakeMove_InvokesEvents()
    {
        var game = new ReversoGameWithEvents();
        AIPlayer firstPlayer = new AIPlayer("A", false);
        AIPlayer secondPlayer = new AIPlayer("B", false);
        bool fieldUpdatedEventRaised = false;
        bool pointsUpdatedEventRaised = false;
        game.FieldUpdated += (updatedField) => fieldUpdatedEventRaised = true;
        game.PointsUpdated += (updatedPoints) => pointsUpdatedEventRaised = true;

        game.StartGame(firstPlayer, secondPlayer);
        game.MakeMove();

        Assert.IsTrue(fieldUpdatedEventRaised);
        Assert.IsTrue(pointsUpdatedEventRaised);
    }

    [Test]
    public void EndGame_InvokesEvent()
    {
        var game = new ReversoGameWithEvents();
        AIPlayer firstPlayer = new AIPlayer("A", false);
        AIPlayer secondPlayer = new AIPlayer("B", false);
        bool gameEndedEventRaised = false;
        game.GameEnded += (winner) => gameEndedEventRaised = winner != null;
        game.StartGame(firstPlayer, secondPlayer);

        while (!game.GetEnded())
        {
            game.MakeMove();
        }

        Assert.IsTrue(gameEndedEventRaised);
    }
}