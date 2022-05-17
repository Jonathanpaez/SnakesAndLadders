
using System;
using EntitiesBE;
using System.Collections.Generic;
using DataAccess.DA;
using Entities.BE;
using System.Linq;

namespace Logic.BL
{
    public class BoardBL
    {
        public BoardBE CreateBoard(List<PlayerBE> players)
        {
            BoardBE board = new BoardBE();
            board.JumpRules = CreateCustomJumpRulesList();
            board.Players = InitPlayers(players);
            return board;
        }
        private List<PlayerBE> InitPlayers(List<PlayerBE> players)
        {
            int initialPosition = 1;
            players.ForEach(player => { player.Position = initialPosition; });
            players[0].HasTurn = true;
            return players;
        }
        public void PrintSpecialBoxes(BoardBE board)
        {
            board.JumpRules.ForEach(snake => {
                string type = snake.Type == BoardDA.BoxesTypes.Snake ? "snake" : "ladder";
                Console.WriteLine("square number " + snake.InitialPosition + ", Type " + type + ", is connected with: " + snake.EndPosition);
            });
        }
        public JumRule GetPlayerMover(BoardBE board, PlayerBE player)
        {
            return board.JumpRules.Find(item => item.InitialPosition == player.Position || item.EndPosition == player.Position);
        }
        private List<JumRule> CreateCustomJumpRulesList()
        {
            List<JumRule> response = new List<JumRule>();
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 2, EndPosition = 38 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 7, EndPosition = 14 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 8, EndPosition = 31 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 15, EndPosition = 26 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 21, EndPosition = 42 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 28, EndPosition = 84 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 36, EndPosition = 44 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 51, EndPosition = 67 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 71, EndPosition = 91 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 78, EndPosition = 98 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Ladder, InitialPosition = 87, EndPosition = 94 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 6, EndPosition = 16 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 11, EndPosition = 49 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 25, EndPosition = 46 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 53, EndPosition = 74 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 60, EndPosition = 64 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 68, EndPosition = 89 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 75, EndPosition = 95 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 80, EndPosition = 99 });
            response.Add(new JumRule { Type = BoardDA.BoxesTypes.Snake, InitialPosition = 88, EndPosition = 92 });
            return response;
        }
        public int MovePlayer(int diceReuslt, BoardBE board, PlayerBE player)
        {
            int response = diceReuslt;
            if (player.Position + diceReuslt > board.WinningSquare) { response = 0; }
            response += player.Position;
            player.Position = response;
            var playerMover = GetPlayerMover(board, player);
            if (playerMover != null)
            {
                response = AutomaticMovement(playerMover, player);
            }
            return response;
        }
        public int AutomaticMovement(JumRule box, PlayerBE player)
        {
            int response;
            if (box.InitialPosition == player.Position && box.Type == BoardDA.BoxesTypes.Ladder)
            {
                Console.WriteLine("Ladder");
                response = box.EndPosition;
            }
            else
            {
                if (box.EndPosition == player.Position && box.Type == BoardDA.BoxesTypes.Snake)
                {
                    Console.WriteLine("Snake");
                    response = box.InitialPosition;
                }
                else
                {
                    response = player.Position;
                }
            }
            return response;
        }
        public bool IsPlayerWinner(BoardBE board, PlayerBE player)
        {
            return board.WinningSquare == player.Position; ;
        }
        public PlayerBE GetNextPlayer(List<PlayerBE> players)
        {
            var currentPlayer = players.Find(player => player.HasTurn);
            currentPlayer.HasTurn = false;
            int nextPlayerID = currentPlayer.Id + 1;
            var nextPlayer = players.Find(player => player.Id == nextPlayerID);
            if (nextPlayer != null)
            {
                nextPlayer.HasTurn = true;
                return nextPlayer;
            }
            else
            {
                players[0].HasTurn = true;
                return players[0];
            }
        }
        public PlayerBE GetCurrentPlayer(List<PlayerBE> players)
        {
            return players.Find(player => player.HasTurn);
        }
    }
}
