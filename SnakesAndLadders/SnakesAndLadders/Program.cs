using System;
using Logic.BL;
using EntitiesBE;
using Entities.BE;

namespace SnakesAndLadders
{
    class Program
    {
        static void Main(string[] args)
        {
            var boardLogic = new BoardBL();
            var PlayerLogic = new PlayerBL();
            var diceLogic = new DiceBL();
            BoardBE board = boardLogic.CreateBoard(PlayerLogic.CreatePlayers(2));
            boardLogic.PrintSpecialBoxes(board);
            int diceResult;
            PlayerBE  currentPlayer = boardLogic.GetCurrentPlayer(board.Players);
            while (!board.HasWinner)
            {
                Console.WriteLine("Turno jugador " + currentPlayer.Id);
                diceResult = diceLogic.RollDice();
                currentPlayer.Position = boardLogic.MovePlayer(
                    diceResult,
                    board,
                    currentPlayer);
                Console.WriteLine("jugador " + currentPlayer.Id + " esta en: " + currentPlayer.Position);
                board.HasWinner = boardLogic.IsPlayerWinner(board, currentPlayer);
                if (!board.HasWinner)
                {
                    currentPlayer = boardLogic.GetNextPlayer(board.Players);
                } else
                {
                    Console.WriteLine("jugador " + currentPlayer.Id + " a Ganado");
                }
            }
        }
    }
}
