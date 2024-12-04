using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;

namespace ChessChallenge.Example
{

    public class EvilBot : IChessBot
    {
    
        public Move Think(Board board, Timer timer)
        {
            Brokenice bot = new Brokenice(board.IsWhiteToMove);
            Candidate move = bot.Think(board,timer);
            return  move.movement;   
        }
    }
}