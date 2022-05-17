using DataAccess.DA;
using Entities.BE;
using EntitiesBE;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.BL
{
    public class PlayerBL
    {
        public List<PlayerBE> CreatePlayers(int participantsNumber)
        {
            List<PlayerBE> response = new List<PlayerBE>();
            for (int i = 0; i < participantsNumber; i++)
            {
                PlayerBE player = new PlayerBE() { Id = i, Position = 0, HasTurn = false };
                response.Add(player);
            }
            return response;
        }
        
    }
}
