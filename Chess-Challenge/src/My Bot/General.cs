using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;

namespace General
{
    public static class Gen
    {
        // Te da todas las piezas que tiene tal jugador. Recomendable pasar como parámetros:
        //   IamWhite==true          y            !IamWhite==true
        // Mirar ejemplo en el bot Timmy
        public static List<Piece> GetAllPieces(Board board, bool whiteTeam)
        {
            List<Piece> localList = new List<Piece>();;
            foreach (PieceType pieceType in Enum.GetValues<PieceType>())
            {
                if (pieceType!= PieceType.None)
                {
                    PieceList localPieceList = board.GetPieceList(pieceType, whiteTeam);
                    foreach (Piece piece in localPieceList) 
                    {
                        localList.Add(piece);
                    }
                }
            }
            return localList;
        }

        // El material de piecesA - el material de PiecesB 
        public static int MaterialDifference(List<Piece> piecesA, List<Piece> piecesB)
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
        public static bool GameIsFinished(Board board)
        {
            if (board.IsInCheckmate() || board.IsInStalemate() || board.IsInsufficientMaterial() || board.IsFiftyMoveDraw())
            {
                Console.WriteLine(board.GameMoveHistory);
                foreach (Move move in board.GameMoveHistory) Console.WriteLine(move.ToString());
                return true;
            }
            else return false;
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

}