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
    public int extraDepth = 4;
    public Dictionary<string, Move[]> maximizingFensAnalized = new Dictionary<string, Move[]>();
    public Dictionary<string, Move[]> minimizingFensAnalized = new Dictionary<string, Move[]>();

    public Candidate Think(Board board, Timer timer)
    {
        return IterativeDeepening(board);
    }

    public Candidate IterativeDeepening(Board board)
    {
        for (int depth = initialDepth; depth<=maxDepth; depth++)
        {
            MiniMax(board, depth, true, new Candidate(Move.NullMove,int.MinValue), int.MinValue, int .MaxValue);
        }
        return new Candidate(maximizingFensAnalized[board.GetFenString()][0],0);
    }
    public List<Candidate> MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate, int alpha, int beta)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            return QuiesceneSearch(board,extraDepth, isMaximizing,lastCandidate, alpha, beta);
        }
        List<Candidate> bestCandidates = new List<Candidate>();
        string fen = board.GetFenString();
        Move[] allLegalMoves = board.GetLegalMoves();
        Move[]? legalMoves = GetAnalizedFenMoves(fen,isMaximizing);
        if (legalMoves==null)
        {
            legalMoves = OrderMoves(allLegalMoves,board);
        }
        else
        {
            List<Move> localMoves = new List<Move>(legalMoves);
            foreach (Move legalMove in allLegalMoves)
            {
                bool found = false;
                foreach (Move analizedMove in legalMoves)
                {
                    if (analizedMove==legalMove) found=true;
                }
                if (!found) localMoves.Add(legalMove);
            }    
            legalMoves=localMoves.ToArray();
        }
        
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            Candidate newCandidate = new Candidate(legalMove,0);
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing, newCandidate, alpha, beta)[0];
            bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
            if (isMaximizing) alpha = Math.Max(alpha, candidate.materialWon);
            else beta = Math.Min(beta,candidate.materialWon);
            board.UndoMove(legalMove);
            if (beta<alpha)
            {
                foreach (Move legalMove2 in legalMoves)
                {
                    bool found = false;
                    foreach (Candidate candidate2 in bestCandidates)
                    {
                        if (candidate2.movement==legalMove2) found = true;
                    }
                    if (!found) bestCandidates.Add(new Candidate(legalMove2,isMaximizing?int.MinValue:int.MaxValue));
                }
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
        // if (noisyMoves==null) noisyMoves=null;
        else noisyMoves=FilterNoisyMoves(board,isWhite,noisyMoves.ToList()).ToArray();
        // Console.WriteLine(noisyMoves.Length + "  dad");
        if (noisyMoves!=null && noisyMoves.Length>0 && depth>0 && !GameIsFinished(board))
        {
            List<Candidate> bestCandidates = new List<Candidate>();
            foreach (Move legalMove in noisyMoves)
            {
                board.MakeMove(legalMove);
                Candidate newCandidate = new Candidate(legalMove,0);
                Candidate candidate = QuiesceneSearch(board, depth-1, !isMaximizing, newCandidate, alpha, beta)[0];
                bestCandidates.Add(new Candidate(legalMove, candidate.materialWon));
                if (isMaximizing) alpha = Math.Max(alpha, candidate.materialWon);
                else beta = Math.Min(beta,candidate.materialWon);
                 
                board.UndoMove(legalMove);
                if (beta<alpha)
                {
                    foreach (Move legalMove2 in noisyMoves)
                    {
                        bool found = false;
                        foreach (Candidate candidate2 in bestCandidates)
                        {
                            if (candidate2.movement==legalMove2) found = true;
                        }
                        if (!found) bestCandidates.Add(new Candidate(legalMove2,isMaximizing?int.MinValue:int.MaxValue));
                    }
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
            if (maximizingFensAnalized.ContainsKey(fen)) return maximizingFensAnalized[fen];
        }
        else
        {
            if (minimizingFensAnalized.ContainsKey(fen)) return minimizingFensAnalized[fen];
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