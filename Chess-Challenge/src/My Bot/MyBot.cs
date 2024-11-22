﻿using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class MyBot : IChessBot
{
        public Move Think(Board board, Timer timer)
        {
            Richard bot = new Richard();
            Move move = bot.Think(board,timer);
            board.MakeMove(move);
            if (GameIsFinished(board))
            {
                Move[] movesHistory = board.GameMoveHistory;
                List<string> stringMoves = new List<string>();
                foreach (Move historyMove in movesHistory)
                {
                    stringMoves.Add((historyMove.ToString()));
                }
                string pgn = GetPgn(stringMoves);
                Console.WriteLine(pgn);
                Console.WriteLine("----------------------------------------------------------------");
            }
            else board.UndoMove(move);
            return  move;
        }
}
