
using System.Collections.Generic;
using DataAccess.DA;
using Entities.BE;

namespace EntitiesBE
{
    public class BoardBE
    {
        public int TotalSquares { get; set; } = 100;
        public int WinningSquare { get; set; } = 100;
        public List<JumRule> JumpRules { get; set; }
        public List<PlayerBE> Players { get; set; }
        public bool HasWinner { get; set; }
    }

    public class JumRule
    {
        public BoardDA.BoxesTypes Type { get; set; }
        public int InitialPosition { get; set; }
        public int EndPosition { get; set; }
    }
}
