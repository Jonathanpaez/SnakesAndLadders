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
            var currentPlayer = boardLogic.GetCurrentPlayer(board.Players);
            // Given the token is on square 1
            Assert.AreEqual(currentPlayer.Position, 1);
            //When the token is moved 3 spaces
            currentPlayer.Position = boardLogic.MovePlayer(
                3,
                board,
                currentPlayer
            );
            // Then the token is on square 4
            Assert.AreEqual(currentPlayer.Position, 4);
        }

        [TestMethod]
        public void TokenThrow2DiceUS1UAT3()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var currentPlayer = boardLogic.GetCurrentPlayer(board.Players);
            // Given the token is on square 1
            Assert.AreEqual(currentPlayer.Position, 1);
            //When the token is moved 3 spaces
            currentPlayer.Position = boardLogic.MovePlayer(
                3,
                board,
                currentPlayer);
            // Then the token is on square 4
            Assert.AreEqual(currentPlayer.Position, 4);
            // And then it is moved 4 spaces
            currentPlayer.Position = boardLogic.MovePlayer(
                4,
                board,
                currentPlayer);
            // Then the token is on square 8
            Assert.AreEqual(currentPlayer.Position, 8);
        }
        [TestMethod]
        public void WinGameUS2UAT1()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var currentPlayer = boardLogic.GetCurrentPlayer(board.Players);
            currentPlayer.Position = boardLogic.MovePlayer(
                96,
                board,
                currentPlayer);
            // Given the token is on square 97
            Assert.AreEqual(currentPlayer.Position, 97);
            // When the token is moved 3 spaces
            currentPlayer.Position = boardLogic.MovePlayer(
                3,
                board,
                currentPlayer);
            // Then the token is on square 100
            Assert.AreEqual(currentPlayer.Position, 100);
            // And the player has won the game
            Assert.AreEqual(boardLogic.IsPlayerWinner(board, currentPlayer), true);
        }
        [TestMethod]
        public void NotWinGameUS2UAT2()
        {
            BoardBE board = boardLogic.CreateBoard(playerLogic.CreatePlayers(2));
            var currentPlayer = boardLogic.GetCurrentPlayer(board.Players);
            currentPlayer.Position = boardLogic.MovePlayer(
                96,
                board,
                currentPlayer);
            // Given the token is on square 97
            Assert.AreEqual(currentPlayer.Position, 97);
            // When the token is moved 4 spaces
            currentPlayer.Position = boardLogic.MovePlayer(
                4,
                board,
                currentPlayer);
            // Then the token is on square 97
            Assert.AreEqual(currentPlayer.Position, 97);
            // And the player has not won the game
            Assert.AreEqual(boardLogic.IsPlayerWinner(board, currentPlayer), false);
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
            var currentPlayer = boardLogic.GetCurrentPlayer(board.Players);
            var initialTokenPosition = currentPlayer.Position;
            // Given the player rolls a 4
            currentPlayer.Position = boardLogic.MovePlayer(
                4,
                board,
                currentPlayer);
            // When they move their token
            // Then the token should move 4 spaces
            Assert.AreEqual(currentPlayer.Position, initialTokenPosition + 4);
        }
    }
}
