using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class Test
{
    public bool isWhite;
    public Random random = new Random();
    public int depth = 4;
    public int extraDepth = 2;
    public Move Think(Board board, Timer timer)
    {
        Move[] legalMoves = board.GetLegalMoves();
        Candidate lastCandidate = new Candidate(legalMoves[0],-1104);
        return MiniMax(board, depth, true, lastCandidate, int.MinValue, int .MaxValue).movement;        
    }

    public Candidate MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            lastCandidate=QuiesceneSearch(board,extraDepth, isMaximizing,lastCandidate, alpha, beta);
            return lastCandidate;
        }
        Candidate bestCandidate = new Candidate(Move.NullMove,isMaximizing ? int.MinValue : int.MaxValue);
        int bestValue = isMaximizing ? int.MinValue : int.MaxValue;
        Move[] legalMoves = board.GetLegalMoves();
        legalMoves = OrderMoves(legalMoves,board);
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            // if (board.IsRepeatedPosition())
            // {
            //     board.UndoMove(legalMove);
            //     continue;
            // }
            Candidate newCandidate = new Candidate(legalMove,0);
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing, newCandidate, alpha, beta);
            if (bestCandidate.movement == Move.NullMove) bestCandidate=new Candidate(legalMove,candidate.materialWon);
            if (isMaximizing)
            {
                if (candidate.materialWon>bestValue)
                {
                    bestValue=candidate.materialWon;
                    bestCandidate = new Candidate(legalMove, candidate.materialWon);
                }
                alpha = Math.Max(alpha, candidate.materialWon);
            }
            else
            {
                if (candidate.materialWon<bestValue)
                {
                    bestValue=candidate.materialWon;
                    bestCandidate = new Candidate(legalMove, candidate.materialWon);
                }
                beta = Math.Min(beta,candidate.materialWon);
            }
            board.UndoMove(legalMove);
            if (beta<alpha)
            {
                break;
            }
        }
        return bestCandidate;
    }

    public Candidate QuiesceneSearch(Board board,int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        List<Move> noisyMoves = GetNoisyMoves(board, isWhite);
        if (noisyMoves.Count>0 && depth>0)
        {
            Candidate bestCandidate = new Candidate(Move.NullMove,isMaximizing ? int.MinValue : int.MaxValue);
            int bestValue = isMaximizing ? int.MinValue : int.MaxValue;
            foreach (Move legalMove in noisyMoves)
            {
                board.MakeMove(legalMove);
                Candidate newCandidate = new Candidate(legalMove,0);
                Candidate candidate = QuiesceneSearch(board, depth-1, !isMaximizing, newCandidate, alpha, beta);
                if (bestCandidate.movement == Move.NullMove) bestCandidate=new Candidate(legalMove,candidate.materialWon);
                if (isMaximizing)
                {
                    if (candidate.materialWon>bestValue)
                    {
                        bestValue=candidate.materialWon;
                        bestCandidate = new Candidate(legalMove, candidate.materialWon);
                    }
                    alpha = Math.Max(alpha, candidate.materialWon);
                }
                else
                {
                    if (candidate.materialWon<bestValue)
                    {
                        bestValue=candidate.materialWon;
                        bestCandidate = new Candidate(legalMove, candidate.materialWon);
                    }
                    beta = Math.Min(beta,candidate.materialWon);
                }
                board.UndoMove(legalMove);
                if (beta<alpha)
                {
                    break;
                }
            }
            return bestCandidate;
        }
        else
        {
            lastCandidate.materialWon=
            MaterialDifference(isWhite,board,depth)+
            PositionalDifference(isWhite,board,depth)+
            RateMovement(lastCandidate.movement,isWhite,board);
            return lastCandidate;
        }
    }
    public Test(bool isWhiteP)
    {
        isWhite=isWhiteP;
    }
}