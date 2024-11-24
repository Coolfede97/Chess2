using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
public class Timmy
{
    public bool isWhite;
    public Move Think(Board board, Timer timer)
    {
        Random random = new Random();
        bool IamWhite = board.IsWhiteToMove ? true : false;
        Move[] moves = board.GetLegalMoves();
        List<Candidate> candidates = new List<Candidate>();
        Candidate candidate = new Candidate(Move.NullMove,-1104);
        foreach (Move move in moves)
        {
            board.MakeMove(move);
            List<Piece> piecesA= GetAllPieces(board,IamWhite==true);
            List<Piece> piecesB= GetAllPieces(board,!IamWhite==true);
            int materialWonOnMove = MaterialDifference(isWhite,board,0);
            if (materialWonOnMove>candidate.materialWon)
            {
                candidates.Clear();
                candidate = new Candidate(move,materialWonOnMove);
                candidates.Add(candidate);
            }
            else if (materialWonOnMove==candidate.materialWon)
            {
                candidate = new Candidate(move,materialWonOnMove);
                candidates.Add(candidate);
            }
            GameIsFinished(board);
            board.UndoMove(move);
        }
        int randomIndex = random.Next(0,candidates.Count);
        candidate = candidates[randomIndex];
        return candidate.movement;
    }
    public Timmy(bool isWhiteP)
    {
        isWhite=isWhiteP;
    }
}