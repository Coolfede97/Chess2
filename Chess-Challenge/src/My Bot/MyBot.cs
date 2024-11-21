using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class MyBot : IChessBot
{
        public Move Think(Board board, Timer timer)
        {
            Timmy bot = new Timmy();
            return bot.Think(board,timer); 
        }
}
