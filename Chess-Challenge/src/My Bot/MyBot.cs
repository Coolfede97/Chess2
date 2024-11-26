using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class MyBot : IChessBot
{
    
        public Move Think(Board board, Timer timer)
        {
            Test bot = new Test(board.IsWhiteToMove);
            Move move = bot.Think(board,timer);
            return  move;
        }
}
