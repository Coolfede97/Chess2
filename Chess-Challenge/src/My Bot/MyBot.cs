using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class MyBot : IChessBot
{
    
        public Move Think(Board board, Timer timer)
        {
            Console.WriteLine(board.GetFenString());
            Richard bot = new Richard(board.IsWhiteToMove);
            Move move = bot.Think(board,timer);
            board.MakeMove(move);
            if (GameIsFinished(board))
            {
                string pgn = GetPgn(board);
                Console.WriteLine(pgn);
                Console.WriteLine("----------------------------------------------------------------");
            }
            else board.UndoMove(move);
            return  move;
        }
}
