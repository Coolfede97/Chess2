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
    public int initialDepth=2;
    public int maxDepth=4;
    public int extraDepth = 2;
    public Dictionary<string, Move[]> maximizingFensAnalized = new Dictionary<string, Move[]>();
    public Dictionary<string, Move[]> minimizingFensAnalized = new Dictionary<string, Move[]>();

    public Candidate Think(Board board, Timer timer)
    {
        return IterativeDeepening(board);
    }

    public Candidate IterativeDeepening(Board board)
    {
        List<Candidate> bestCandidates = new List<Candidate>();
        for (int depth = initialDepth; depth<=maxDepth; depth++)
        {
            bestCandidates = MiniMax(board, depth, true, new Candidate(Move.NullMove,int.MinValue), int.MinValue, int .MaxValue);
        }
        return bestCandidates[0];
    }
    public List<Candidate> MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            return QuiesceneSearch(board,extraDepth, isMaximizing,lastCandidate, alpha, beta);
        }
        List<Candidate> bestCandidates = new List<Candidate>();
        string fen = board.GetFenString();
        Move[]? legalMoves = GetAnalizedFenMoves(fen,isMaximizing);
        if (legalMoves==null)
        {
            legalMoves = board.GetLegalMoves();
            legalMoves = OrderMoves(legalMoves,board);
        }
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            Candidate newCandidate = new Candidate(legalMove,0);
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing, newCandidate, alpha, beta)[0];
            if (bestCandidates.Count==0) bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
            if (isMaximizing) alpha = Math.Max(alpha, candidate.materialWon);
            else beta = Math.Min(beta,candidate.materialWon);
            bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
            board.UndoMove(legalMove);
            if (beta<alpha)
            {
                break;
            }
        }
        if (isMaximizing) bestCandidates = SortByMaterialDescending(bestCandidates);
        else bestCandidates = SortByMaterialAscending(bestCandidates);
        UpdateFensAnalized(fen,bestCandidates,isMaximizing);
        return bestCandidates;
    }

    public List<Candidate> QuiesceneSearch(Board board,int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        string fen = board.GetFenString();
        Move[]? noisyMoves = GetAnalizedFenMoves(fen,isMaximizing);
        if (noisyMoves==null) noisyMoves=GetNoisyMoves(board, isWhite).ToArray();
        else noisyMoves=FilterNoisyMoves(board,isWhite,noisyMoves.ToList()).ToArray();
        if (noisyMoves.Length>0 && depth>0)
        {
            List<Candidate> bestCandidates = new List<Candidate>();
            foreach (Move legalMove in noisyMoves)
            {
                board.MakeMove(legalMove);
                Candidate newCandidate = new Candidate(legalMove,0);
                Candidate candidate = QuiesceneSearch(board, depth-1, !isMaximizing, newCandidate, alpha, beta)[0];
                if (bestCandidates.Count==0) bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
                if (isMaximizing) alpha = Math.Max(alpha, candidate.materialWon);
                else beta = Math.Min(beta,candidate.materialWon);
                bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                 
                board.UndoMove(legalMove);
                if (beta<alpha)
                {
                    break;
                }
            }
            if (isMaximizing) bestCandidates = SortByMaterialDescending(bestCandidates);
            else bestCandidates = SortByMaterialAscending(bestCandidates);
            UpdateFensAnalized(fen,bestCandidates,isMaximizing);
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
    
    public Move[]? GetAnalizedFenMoves(string fen, bool isMaximizing)
    {
        if (isMaximizing)
        {
            if (maximizingFensAnalized.TryGetValue(fen, out var moves))
            {
                return moves;
            }   
        }
        else
        {
            if (minimizingFensAnalized.TryGetValue(fen, out var moves))
            {
                return moves;
            }
        }
        return null;
    }
    
    public void UpdateFensAnalized(string fen, List<Candidate> candidates, bool isMaximizing)
    {
        if (isMaximizing)
        {
            if (maximizingFensAnalized.ContainsKey(fen)) maximizingFensAnalized[fen] = CandidatesToArray(candidates);
            else maximizingFensAnalized.Add(fen,CandidatesToArray(candidates));
        }
        else
        {
            if (minimizingFensAnalized.ContainsKey(fen)) minimizingFensAnalized[fen] = CandidatesToArray(candidates);
            else minimizingFensAnalized.Add(fen,CandidatesToArray(candidates));
        }
    }
    public Test(bool isWhiteP)
    {
        isWhite=isWhiteP;
    }
}