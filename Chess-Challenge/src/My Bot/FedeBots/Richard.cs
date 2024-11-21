using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class Richard    
{
    public Move Think(Board board, Timer timer)
    {
        int depth = 3;
        int currentCycle=0;
        while(currentCycle<depth)
        {
            
        }
    }

    public Candidate MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            return lastCandidate;
        }
        Candidate bestCandidate = new Candidate(Move.NullMove, 0);
        Move[] legalMoves = board.GetLegalMoves();
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing,)
        }
    }
}
