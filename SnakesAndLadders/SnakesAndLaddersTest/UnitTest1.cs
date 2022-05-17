using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Logic.BL;
using EntitiesBE;
namespace SnakesAndLaddersTest
{
    [TestClass]
    public class UnitTest1
    {
        BoardBL boardLogic = new BoardBL();
        PlayerBL playerLogic = new PlayerBL();
        DiceBL diceLogic = new DiceBL();
        [TestMethod]
        public void PlayerSquare1US1UAT1()
        {
            // Given the game is started
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            board.Players.ForEach(player => {
                //Then the token is on square 1
                Assert.AreEqual(player.Position, 1);
            });
        }
        [TestMethod]
        public void TokenMove3SpacesUS1UAT2()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var playerTurn = board.Players.Find(player => player.HasTurn);
            // Given the token is on square 1
            Assert.AreEqual(playerTurn.Position, 1);
            //When the token is moved 3 spaces
            playerTurn.Position = boardLogic.MovePlayer(
                3,
                board,
                playerTurn
            );
            // Then the token is on square 4
            Assert.AreEqual(playerTurn.Position, 4);
        }

        [TestMethod]
        public void TokenThrow2DiceUS1UAT3()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var playerTurn = board.Players.Find(player => player.HasTurn);
            // Given the token is on square 1
            Assert.AreEqual(playerTurn.Position, 1);
            //When the token is moved 3 spaces
            playerTurn.Position = boardLogic.MovePlayer(
                3,
                board,
                playerTurn);
            // Then the token is on square 4
            Assert.AreEqual(playerTurn.Position, 4);
            // And then it is moved 4 spaces
            playerTurn.Position = boardLogic.MovePlayer(
                4,
                board,
                playerTurn);
            // Then the token is on square 8
            Assert.AreEqual(playerTurn.Position, 8);
        }
        [TestMethod]
        public void WinGameUS2UAT1()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var playerTurn = board.Players.Find(player => player.HasTurn);
            playerTurn.Position = boardLogic.MovePlayer(
                96,
                board,
                playerTurn);
            // Given the token is on square 97
            Assert.AreEqual(playerTurn.Position, 97);
            // When the token is moved 3 spaces
            playerTurn.Position = boardLogic.MovePlayer(
                3,
                board,
                playerTurn);
            // Then the token is on square 100
            Assert.AreEqual(playerTurn.Position, 100);
            // And the player has won the game
            Assert.AreEqual(boardLogic.IsPlayerWinner(board, playerTurn), true);
        }
        [TestMethod]
        public void NotWinGameUS2UAT2()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var playerTurn = board.Players.Find(player => player.HasTurn);
            playerTurn.Position = boardLogic.MovePlayer(
                96,
                board,
                playerTurn);
            // Given the token is on square 97
            Assert.AreEqual(playerTurn.Position, 97);
            // When the token is moved 4 spaces
            playerTurn.Position = boardLogic.MovePlayer(
                4,
                board,
                playerTurn);
            // Then the token is on square 100
            Assert.AreEqual(playerTurn.Position, 97);
            // And the player has won the game
            Assert.AreEqual(boardLogic.IsPlayerWinner(board, playerTurn), false);
        }
        [TestMethod]
        public void RollDiceUS3UAT1()
        {
            // Given the game is started
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            // When the player rolls a die
            int diceReturn = diceLogic.RollDice();
            // Then the result should be between 1-6 inclusive
            Assert.IsTrue(1 <= diceReturn && diceReturn <= 6);
        }
        [TestMethod]
        public void Move4ApaceUS3UAT2()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var playerTurn = board.Players.Find(player => player.HasTurn);
            var initialTokenPosition = playerTurn.Position;
            // Given the player rolls a 4
            playerTurn.Position = boardLogic.MovePlayer(
                4,
                board,
                playerTurn);
            // When they move their token
            // Then the token should move 4 spaces
            Assert.AreEqual(playerTurn.Position, initialTokenPosition + 4);
        }
    }
}
