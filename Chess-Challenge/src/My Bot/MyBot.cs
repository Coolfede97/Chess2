using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        Candidate candidate = new Candidate(Move.NullMove,-1104);
        foreach (Move move in moves)
        {
            board.MakeMove(move);
            PieceList piecesA;
            PieceList piecesB;
            
            PieceList localPieceList = board.GetPieceList(PieceType.None, )
        }

        public PieceList GetAllPieces(Board board, bool whiteTeam)
        {
            PieceList localList;
            foreach (PieceType pieceType in Enum.GetValues<PieceType>())
            {
                PieceList localPieceList = board.GetPieceList(pieceType, whiteTeam);
                foreach (Piece piece in localPieceList) localList.Append(piece)
            }
        }
        public int MaterialDifference(PieceList piecesA, PieceList piecesB)
        {
            int materialA = CalculateMaterial(piecesA);
            int materialB = CalculateMaterial(piecesB);

            int CalculateMaterial(PieceList piecesList)
            {
                int localMaterial=0;
                foreach(Piece piece in piecesList)
                {
                    if (piece.IsPawn) localMaterial++;
                    else if (piece.IsBishop || piece.IsKnight) localMaterial+=3;
                    else if  (piece.IsRook) localMaterial+=5;
                    else if (piece.IsQueen) localMaterial+=9;
                }
                return localMaterial;
            }

            return materialA-materialB;
        }
    }
    public class Candidate
    {
        Move movement;
        int materialWon;

        public Candidate(Move move, int material)
        {
            movement=move;
            materialWon=material;
        }
    }
}