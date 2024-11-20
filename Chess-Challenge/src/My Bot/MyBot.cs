using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;

public class MyBot : IChessBot
{
        public Move Think(Board board, Timer timer)
        {
            bool IamWhite;
            if (board.IsWhiteToMove) IamWhite=true;
            else IamWhite=false;
            Move[] moves = board.GetLegalMoves();
            Candidate candidate = new Candidate(Move.NullMove,-1104);
            foreach (Move move in moves)
            {
                board.MakeMove(move);
                List<Piece> piecesA=GetAllPieces(board,IamWhite==true);
                List<Piece> piecesB=GetAllPieces(board,!IamWhite==true);
                int materialWonOnMove = MaterialDifference(piecesA, piecesB);
                if (materialWonOnMove>candidate.materialWon)
                {
                    candidate = new Candidate(move,materialWonOnMove);
                }
            }
            return candidate.movement;
        }

        public List<Piece> GetAllPieces(Board board, bool whiteTeam)
        {
            List<Piece> localList = new List<Piece>();;
            foreach (PieceType pieceType in Enum.GetValues<PieceType>())
            {
                if (pieceType!= PieceType.None)
                {
                    PieceList localPieceList = board.GetPieceList(pieceType, whiteTeam);
                    foreach (Piece piece in localPieceList) localList.Add(piece);
                }
            }
            return localList;
        }
        public int MaterialDifference(List<Piece> piecesA, List<Piece> piecesB)
        {
            int materialA = CalculateMaterial(piecesA);
            int materialB = CalculateMaterial(piecesB);

            int CalculateMaterial(List<Piece> piecesList)
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

    public class Candidate
    {
        public Move movement;
        public int materialWon;

        public Candidate(Move move, int material)
        {
            movement=move;
            materialWon=material;
        }
    }
}
