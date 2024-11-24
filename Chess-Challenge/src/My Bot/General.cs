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
        public static int MaterialDifference(bool isWhite, Board board, int depth)
        {
            if (board.IsInCheckmate())
            {
                return isWhite!=board.IsWhiteToMove ? 1104+depth : -1104-depth;
            }
            List<Piece> piecesA;
            List<Piece> piecesB;
            if (isWhite)
            {
                piecesA = GetAllPieces(board, true);
                piecesB = GetAllPieces(board, false);
            }
            else
            {
                piecesA = GetAllPieces(board, false);
                piecesB = GetAllPieces(board, true);
            }
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
                    else if (piece.IsKing) localMaterial+=1104;
                }
                return localMaterial;
            }

            return materialA-materialB;
        }
        public static bool GameIsFinished(Board board)
        {
            if (board.IsInCheckmate() || board.IsInStalemate() || board.IsInsufficientMaterial() || board.IsFiftyMoveDraw())
            {
                return true;
            }
            else return false;
        }

        public static string GetPgn(List<string> movements)
        {
            List<string> movimientosExtraidos = new List<string>();

            foreach (var movement in movements)
            {
                int startIndex = movement.IndexOf('\'') + 1;
                int endIndex = movement.LastIndexOf('\''); 

                if (startIndex > 0 && endIndex > startIndex)
                {
                    string movimientoExtraido = movement.Substring(startIndex, endIndex - startIndex);
                    movimientosExtraidos.Add(movimientoExtraido);
                }
            }
            return string.Join(" ", movimientosExtraidos);
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