using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
using static DatabaseManager;
public class Test
{
    public bool isWhite;
    public Random random = new Random();
    public int depth = 4;
    public int extraDepth = 2;
    public Candidate Think(Board board, Timer timer)
    {
        return MiniMax(board, depth, true, new Candidate(Move.NullMove,int.MinValue), int.MinValue, int .MaxValue)[0];        
    }

    public List<Candidate> MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            return QuiesceneSearch(board,extraDepth, isMaximizing,lastCandidate, alpha, beta);
        }
        List<Candidate> bestCandidates = new List<Candidate>();
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
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing, newCandidate, alpha, beta)[0];
            if (bestCandidates.Count==0) bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
            if (isMaximizing)
            {
                if (candidate.materialWon>bestCandidates[0].materialWon)
                {
                    bestCandidates.Insert(0,new Candidate(legalMove, candidate.materialWon));
                }
                else
                {
                    bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                }
                alpha = Math.Max(alpha, candidate.materialWon);
            }
            else
            {
                if (candidate.materialWon<bestCandidates[0].materialWon)
                {
                    bestCandidates.Insert(0,new Candidate(legalMove, candidate.materialWon));
                }
                else
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
        return bestCandidates;
    }

    public List<Candidate> QuiesceneSearch(Board board,int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        List<Move> noisyMoves = GetNoisyMoves(board, isWhite);
        if (noisyMoves.Count>0 && depth>0)
        {
            List<Candidate> bestCandidates = new List<Candidate>();
            foreach (Move legalMove in noisyMoves)
            {
                board.MakeMove(legalMove);
                Candidate newCandidate = new Candidate(legalMove,0);
                Candidate candidate = QuiesceneSearch(board, depth-1, !isMaximizing, newCandidate, alpha, beta)[0];
                if (bestCandidates.Count==0) bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
                if (isMaximizing)
                {
                    if (candidate.materialWon>bestCandidates[0].materialWon)
                    {
                        bestCandidates.Insert(0,new Candidate(legalMove, candidate.materialWon));
                    }
                    else
                    {
                        bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                    }
                    alpha = Math.Max(alpha, candidate.materialWon);
                }
                else
                {
                    if (candidate.materialWon<bestCandidates[0].materialWon)
                    {
                        bestCandidates.Insert(0,new Candidate(legalMove, candidate.materialWon));
                    }
                    else
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
            return bestCandidates;
        }
        else
        {
            lastCandidate.materialWon=
            MaterialDifference(isWhite,board,depth)+
            PositionalDifference(isWhite,board,depth)+
            RateMovement(lastCandidate.movement,isWhite,board);
            List<Candidate> localList = new List<Candidate>();
            localList.Add(lastCandidate);
            return localList;
        }
    }
    public Test(bool isWhiteP)
    {
        isWhite=isWhiteP;
    }
}