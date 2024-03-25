namespace ReversoGame.Model.Tests;
using NUnit.Framework;
using Reverso.Model;
using System;
using System.Collections.Generic;

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

        Assert.AreNotEqual(startingPoints, firstPlayer.GetPoints());
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

        Assert.AreNotEqual(startingPoints, firstPlayer.GetPoints());
    }

    [Test]
    public void MakeMoves_FirstPlayerWins_WithThirteenPoints()
    {
        var game = new ReversoGame();
        TestMovesHandler movesHandler = new TestMovesHandler();
        HumanPlayer firstPlayer = new HumanPlayer("A", movesHandler);
        HumanPlayer secondPlayer = new HumanPlayer("B", movesHandler);
        game.StartGame(firstPlayer, secondPlayer);

        //Moves that should result in game ending with A winning
        for (int i = 0; i < movesHandler.GetMovesLength(); i++)
        {
            game.MakeMove();
        }

        Assert.AreEqual(game.GetWinner().GetName(), "A");
        Assert.AreEqual(firstPlayer.GetPoints(), 13);
        Assert.AreEqual(secondPlayer.GetPoints(), 0);
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
        game.FieldUpdated += (field) => fieldUpdatedEventRaised = true;
        game.PointsUpdated += (points) => pointsUpdatedEventRaised = true;

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
        game.GameEnded += (player) => gameEndedEventRaised = true;
        game.StartGame(firstPlayer, secondPlayer);

        while (!game.GetEnded())
        {
            game.MakeMove();
        }

        Assert.IsTrue(gameEndedEventRaised);
    }
}