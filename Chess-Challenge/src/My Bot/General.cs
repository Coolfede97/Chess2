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
                return isWhite!=board.IsWhiteToMove ? 100_000+depth : -100_000-depth;
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
                    if (piece.IsPawn) localMaterial+=100;
                    else if (piece.IsBishop || piece.IsKnight) localMaterial+=300;
                    else if  (piece.IsRook) localMaterial+=500;
                    else if (piece.IsQueen) localMaterial+=900;
                }
                return localMaterial;
            }

            return materialA-materialB;
        }
        // public static int PositionDifference(bool isWhite, Board board, int depth)
        // {
        //     List<Piece> piecesA = GetAllPieces(board,isWhite);
        //     List<Piece> piecesB = GetAllPieces(board, !isWhite);
        //     int gamePhase = DeterminateGamePhase(board);
            
            
        // }

        // 0 = Early    1 Mid      2 End
        public static int DeterminateGamePhase(Board board)
        {
            int piecesOnBoard=BitboardHelper.GetNumberOfSetBits(board.AllPiecesBitboard);
            // foreach (PieceType pieceType in Enum.GetValues<PieceType>())
            // {
            //     if (pieceType!= PieceType.None)
            //     {
            //         PieceList localPieceListWhite = board.GetPieceList(pieceType, true);
            //         PieceList localPieceListBlack = board.GetPieceList(pieceType, false);
            //         foreach (Piece piece in localPieceListWhite) piecesOnBoard++;
            //         foreach (Piece piece in localPieceListBlack) piecesOnBoard++;
            //     }
            // }
            if (piecesOnBoard>20) return 0;
            if (piecesOnBoard>10) return 1;
            return 2;
        }
        public static Move[] OrderMoves(Move[] moves, Board board)
        {
            return moves.OrderByDescending
            (move=>
                {
                    int score = 0;
                    board.MakeMove(move);
                    if (board.IsInCheck()) score+=10_000;
                    board.UndoMove(move);
                    if (move.IsCapture) score+=9_000;
                    if (move.IsPromotion) score+=5_000;
                    if (board.SquareIsAttackedByOpponent(move.TargetSquare)) score += 1_000;
                    return score;
                }
            ).ToArray();
        }
        public static bool GameIsFinished(Board board)
        {
            if (board.IsInCheckmate() || board.IsInStalemate() || board.IsInsufficientMaterial() || board.IsFiftyMoveDraw())
            {
                return true;
            }
            else return false;
        }

        public static string GetPgn(Board board)
        {
            Move[] movesHistory = board.GameMoveHistory;
            List<string> stringMoves = new List<string>();
            foreach (Move historyMove in movesHistory)
            {
                stringMoves.Add(historyMove.ToString());
            }
            List<string> movimientosExtraidos = new List<string>();

            foreach (var movement in stringMoves)
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