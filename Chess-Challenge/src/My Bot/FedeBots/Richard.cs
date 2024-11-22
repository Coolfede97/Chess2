using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class Richard    
{
    public Random random = new Random();
    public Move Think(Board board, Timer timer)
    {
        int depth = 2;
        Move[] legalMoves = board.GetLegalMoves();
        Candidate lastCandidate = new Candidate(legalMoves[0],-1104);
        return MiniMax(board, depth, !board.IsWhiteToMove, lastCandidate).movement;        
    }

    public Candidate MiniMax(Board board, int depth, bool isMaximizing, Candidate lastCandidate)
    {
        if (depth == 0 || GameIsFinished(board))
        {
            List<Piece> piecesA = GetAllPieces(board, true);
            List<Piece> piecesB = GetAllPieces(board, false);
            lastCandidate.materialWon=MaterialDifference(piecesA,piecesB);
            return lastCandidate;
        }
        List<Candidate> bestCandidates = new List<Candidate>();
        Move[] legalMoves = board.GetLegalMoves();
        bestCandidates.Add(new Candidate(legalMoves[0], -1104));
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            Candidate newCandidate = new Candidate(legalMove,-1104);
            Candidate candidate = MiniMax(board, depth-1, !isMaximizing, newCandidate);
            if (isMaximizing && candidate.materialWon>bestCandidates[0].materialWon)
            {
                bestCandidates.Clear();
                bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
            }
            else if (!isMaximizing && candidate.materialWon<bestCandidates[0].materialWon)
            {
                bestCandidates.Clear();
                bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
            }
            else if (candidate.materialWon==bestCandidates[0].materialWon) bestCandidates.Add(new Candidate(legalMove,candidate.materialWon));
            board.UndoMove(legalMove);
        }
        int randomIndex = random.Next(0,bestCandidates.Count);
        return bestCandidates[randomIndex];
    }
}
