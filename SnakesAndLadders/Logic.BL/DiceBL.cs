using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.BL
{
    public class DiceBL
    {
        public int RollDice()
        {
            Random rd = new Random();
            int response = rd.Next(1, 7);
            Console.WriteLine("Resultado del dado: " + response);
            return response;
        }
    }
}
