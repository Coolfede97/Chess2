using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class Richard    
{
    public bool isWhite;
    public Random random = new Random();
    public Candidate Think(Board board, Timer timer)
    {
        int depth = 4;
        Move[] legalMoves = board.GetLegalMoves();
        Candidate lastCandidate = new Candidate(legalMoves[0],-1104);
        Candidate move = MiniMax(board, depth, true, lastCandidate, int.MinValue, int .MaxValue);
        return move;
    }

    public Candidate MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            lastCandidate.materialWon=MaterialDifference(isWhite,board,depth);
            return lastCandidate;
        }
        List<Candidate> bestCandidates = new List<Candidate>();
        int bestValue = isMaximizing ? int.MinValue : int.MaxValue;
        Move[] legalMoves = board.GetLegalMoves();
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            Candidate newCandidate = new Candidate(legalMove,-1104);
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing, newCandidate, alpha, beta);

            if (isMaximizing)
            {
                if (candidate.materialWon>bestValue)
                {
                    bestValue=candidate.materialWon;
                    bestCandidates.Clear();
                    bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                }
                else if (candidate.materialWon==bestValue)
                {
                    bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                }
                alpha = Math.Max(alpha, candidate.materialWon);
            }
            else
            {
                if (candidate.materialWon<bestValue)
                {
                    bestValue=candidate.materialWon;
                    bestCandidates.Clear();
                    bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                }
                else if (candidate.materialWon==bestValue)
                {
                    bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                }
                beta = Math.Min(beta,candidate.materialWon);
            }
            board.UndoMove(legalMove);
            if (beta<alpha)
            {
                break;
            }
        }
        int randomIndex = random.Next(0,bestCandidates.Count);
        return bestCandidates[randomIndex];
    }
    public Richard(bool isWhiteP)
    {
        isWhite=isWhiteP;
    }
}
