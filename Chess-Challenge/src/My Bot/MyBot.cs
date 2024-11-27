using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
using static DatabaseManager;
public class MyBot : IChessBot
{
    public int moveCount=0;
    public bool openingSaved=false;
    public List<FenMove> openingHistory = new List<FenMove>();
    public Move Think(Board board, Timer timer)
    {
        Brokenice bot = new Brokenice(board.IsWhiteToMove);
        Candidate move = bot.Think(board,timer);
        if (!openingSaved && moveCount>=6)
        {
            openingSaved=true;
            foreach (FenMove fenMove in openingHistory)
            {
                fenMove.MaterialWon=move.materialWon;
            }
            List<FenMove> aaa = LoadFenMoveList();
            Console.WriteLine(aaa[0].Fen);
            Console.WriteLine(aaa[0].BestMove);
        }
        else
        {
            FenMove newFenMove = new FenMove(board.GetFenString(),move.movement,move.materialWon);
            openingHistory.Add(newFenMove);
        }
        // Console.WriteLine(move.movement.ToString());
        moveCount++;
        return  move.movement;
    }
}
