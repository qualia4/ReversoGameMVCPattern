namespace ReversoGame.Model.Tests;
using NUnit.Framework;
using Reverso.Model;
using System;
using System.Collections.Generic;

public class ReversoModelTests
{
    [Test]
    public void MakeMove_WhenHumanPlayer_DoesMakeMove()
    {
        var game = new ReversoGameWithEvents();
        TestMovesHandler movesHandler = new TestMovesHandler();
        HumanPlayer firstPlayer = new HumanPlayer("A");
        firstPlayer.SetCommandHandler(movesHandler);
        AIPlayer secondPlayer = new AIPlayer("B");
        game.StartGame(firstPlayer, secondPlayer);
        var startingPoints = firstPlayer.GetPoints();

        game.MakeMove();

        Assert.AreNotEqual(startingPoints, firstPlayer.GetPoints());
    }

    [Test]
    public void MakeMove_WhenBot_DoesMakeMove()
    {
        var game = new ReversoGameWithEvents();
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
        var game = new ReversoGameWithEvents();
        TestMovesHandler movesHandler = new TestMovesHandler();
        HumanPlayer firstPlayer = new HumanPlayer("A");
        HumanPlayer secondPlayer = new HumanPlayer("B");
        firstPlayer.SetCommandHandler(movesHandler);
        secondPlayer.SetCommandHandler(movesHandler);
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
    public void StartGame_InvokesEvents()
    {
        var game = new ReversoGameWithEvents();
        AIPlayer firstPlayer = new AIPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        bool gameStartedEventRaised = false;
        game.GameStarted += () => gameStartedEventRaised = true;

        game.StartGame(firstPlayer, secondPlayer);

        Assert.IsTrue(gameStartedEventRaised);
    }


    [Test]
    public void MakeMove_InvokesEvents()
    {
        var game = new ReversoGameWithEvents();
        AIPlayer firstPlayer = new AIPlayer("A");
        AIPlayer secondPlayer = new AIPlayer("B");
        bool fieldUpdatedEventRaised = false;
        bool pointsUpdatedEventRaised = false;
        game.FieldUpdated += (field) => fieldUpdatedEventRaised = true;
        game.PointsUpdated += (points) => pointsUpdatedEventRaised = true;

        game.StartGame(firstPlayer, secondPlayer);
        game.MakeMove();

        Assert.IsTrue(fieldUpdatedEventRaised);
        Assert.IsTrue(pointsUpdatedEventRaised);
    }
}

public class TestMovesHandler : IInputHandler
{
    private int moveIndex;
    private int[,] moves =  { { 5, 4 }, { 3, 5 }, { 2, 4 }, { 5, 5 }, {4, 6}, {5, 3}, {6, 4}, {4, 5}, {4, 2} };

    public TestMovesHandler()
    {
        moveIndex = 0;
    }
    public int[] GetPlayerCoords(Field GameField)
    {
        int[] coords = new int[2] {moves[moveIndex, 0], moves[moveIndex, 1]};
        moveIndex++;
        return coords;
    }

    public int GetMovesLength()
    {
        return moves.GetLength(0);
    }
}