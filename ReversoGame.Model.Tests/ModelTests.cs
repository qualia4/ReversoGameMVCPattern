using NUnit.Framework;
using Reverso.Model;
using System;
using System.Collections.Generic;


namespace ReversoGame.Model.Tests;

[TestFixture]
public class ModelTests
{

    [Test]
    public void MakeMove_WithOutOfFieldCoordinates_DoesNotChangePoints()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);
        game.StartGame(false);
        int initialPointsPlayer1 = firstPlayer.GetPoints();
        int initialPointsPlayer2 = secondPlayer.GetPoints();

        game.MakeMove(-1, -1);

        Assert.AreEqual(initialPointsPlayer1, firstPlayer.GetPoints());
        Assert.AreEqual(initialPointsPlayer2, secondPlayer.GetPoints());
    }

    [Test]
    public void MakeMove_WithInvalidCoordinates_DoesNotChangePoints()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);
        game.StartGame(false);
        game.MakeMove(5, 4);
        int initialPointsFirstPlayer = firstPlayer.GetPoints();
        int initialPointsSecondPlayer = secondPlayer.GetPoints();

        game.MakeMove(5,4);

        Assert.AreEqual(initialPointsFirstPlayer, firstPlayer.GetPoints());
        Assert.AreEqual(initialPointsSecondPlayer, secondPlayer.GetPoints());
    }

    [Test]
    public void MakeMove_WithInvalidCoordinates_DoesChangeField()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);
        game.StartGame(false);
        int initialPointsPlayer1 = firstPlayer.GetPoints();
        int initialPointsPlayer2 = secondPlayer.GetPoints();

        game.MakeMove(5, 4);

        Assert.AreNotEqual(initialPointsPlayer1, firstPlayer.GetPoints());
        Assert.AreNotEqual(initialPointsPlayer2, secondPlayer.GetPoints());
    }

    [Test]
    public void MakeMoves_FirstPlayerWins_WithThirteenPoints()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);
        game.StartGame(false);

        //Moves that should result in game ending with A winning
        int[,] moves =  { { 5, 4 }, { 3, 5 }, { 2, 4 }, { 5, 5 }, {4, 6}, {5, 3}, {6, 4}, {4, 5}, {4, 2} };
        for (int i = 0; i < moves.GetLength(0); i++)
        {
            game.MakeMove(moves[i,0], moves[i,1]);
        }

        Assert.AreEqual(game.GetWinner().GetName(), "A");
        Assert.AreEqual(firstPlayer.GetPoints(), 13);
        Assert.AreEqual(secondPlayer.GetPoints(), 0);
    }

    [Test]
    public void IsValidMove_WhenGameHasNotStarted_ThrowsException()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);

        Assert.Throws<Exception>(() => game.IsValidMove(0,0));
    }

    [Test]
    public void MakeMove_WhenPvETrue_DoesMakeRandomMove()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);
        var secondMovePoints = 4;

        game.StartGame(true);
        game.MakeMove(5, 4);

        Assert.AreNotEqual(secondMovePoints, firstPlayer.GetPoints());
    }

    [Test]
    public void StartGame_MakeMove_InvokesEvents()
    {
        Player firstPlayer = new Player("A");
        Player secondPlayer = new Player("B");
        var game = new ReversoGameWithEvents(firstPlayer, secondPlayer);
        bool gameStartedEventRaised = false;
        bool fieldUpdatedEventRaised = false;
        bool pointsUpdatedEventRaised = false;
        game.GameStarted += () => gameStartedEventRaised = true;
        game.FieldUpdated += (field) => fieldUpdatedEventRaised = true;
        game.PointsUpdated += (points) => pointsUpdatedEventRaised = true;

        game.StartGame(false);
        game.MakeMove(5,4);

        Assert.IsTrue(gameStartedEventRaised);
        Assert.IsTrue(fieldUpdatedEventRaised);
        Assert.IsTrue(pointsUpdatedEventRaised);
    }




}