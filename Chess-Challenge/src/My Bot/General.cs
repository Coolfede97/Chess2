using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static PositionData.PosData;
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
        public static int RateMovement(Move move, bool isWhite, Board board)
        {
            int RatingPlus = 0;
            if (move.IsCapture)
            {
                RatingPlus+= isWhite!=board.IsWhiteToMove ? 1 : -1;
            }
            if (board.IsInCheck())
            {
                RatingPlus+= isWhite!=board.IsWhiteToMove ? 2 : -2;
            }
            return RatingPlus;
        }
        public static int MaterialDifference(bool isWhite, Board board, int depth)
        {
            if (board.IsInCheckmate())
            {
                return isWhite!=board.IsWhiteToMove ? 100_000+depth*1000 : -100_000-depth*1000;
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
                    int gamePhase = DeterminateGamePhase(board);
                    // Valores sacados de: https://blog.mathieuacher.com/ChessPiecesValues/
                    if (gamePhase==0 || gamePhase==1)
                    {
                        if (piece.IsPawn) localMaterial+=82;
                        else if (piece.IsKnight) localMaterial+=337;
                        else if (piece.IsBishop) localMaterial+=365;
                        else if  (piece.IsRook) localMaterial+=477;
                        else if (piece.IsQueen) localMaterial+=1025;
                    }
                    else
                    {
                        if (piece.IsPawn) localMaterial+=94;
                        else if (piece.IsKnight) localMaterial+=281;
                        else if (piece.IsBishop) localMaterial+=297;
                        else if  (piece.IsRook) localMaterial+=512;
                        else if (piece.IsQueen) localMaterial+=936;
                    }
                }
                return localMaterial;
            }

            return materialA-materialB;
        }
        public static int PositionalDifference(bool isWhite, Board board, int depth)
        {
            List<Piece> piecesA = GetAllPieces(board,isWhite);
            List<Piece> piecesB = GetAllPieces(board, !isWhite);
            int gamePhase = DeterminateGamePhase(board);
            int valueA = PositionValue(piecesA,false,gamePhase);
            int valueB = PositionValue(piecesB, true, gamePhase);
            return valueA-valueB;
        }
        private static int PositionValue(List<Piece> pieces, bool oponent, int gamePhase)
        {
            int totalPositionValue = 0;

            foreach (Piece piece in pieces)
            {
                // Obtener el índice de la posición de la pieza
                int squareIndex = piece.Square.Index;

                // Tabla que se usará para esta pieza
                int[] table={};

                // Seleccionar tabla según el tipo de pieza y fase del juego
                if (piece.PieceType == PieceType.Pawn)
                {
                    table = gamePhase == 0 ? (oponent ? BPawnEarly : APawnEarly)
                        : gamePhase == 1 ? (oponent ? BPawnMid : APawnMid)
                        : (oponent ? BPawnEnd : APawnEnd);
                }
                else if (piece.PieceType == PieceType.Knight)
                {
                    table = gamePhase == 0 ? (oponent ? BKnightEarly : AKnightEarly)
                        : gamePhase == 1 ? (oponent ? BKnightMid : AKnightMid)
                        : (oponent ? BKnightLate : AKnightEnd);
                }
                else if (piece.PieceType == PieceType.Bishop)
                {
                    table = gamePhase == 0 ? (oponent ? BBishopEarly : ABishopEarly)
                        : gamePhase == 1 ? (oponent ? BBishopMid : ABishopMid)
                        : (oponent ? BBishopEnd : ABishopEnd);
                }
                else if (piece.PieceType == PieceType.Rook)
                {
                    table = gamePhase == 0 ? (oponent ? BRookEarly : ARookEarly)
                        : gamePhase == 1 ? (oponent ? BRookMid : ARookMid)
                        : (oponent ? BRookEnd : ARookEnd);
                }
                else if (piece.PieceType == PieceType.Queen)
                {
                    table = gamePhase == 0 ? (oponent ? BQueenEarly : AQueenEarly)
                        : gamePhase == 1 ? (oponent ? BQueenMid : AQueenMid)
                        : (oponent ? BQueenEnd : AQueenEnd);
                }
                else if (piece.PieceType == PieceType.King)
                {
                    table = gamePhase == 0 ? (oponent ? BKingEarly : AKingEarly)
                        : gamePhase == 1 ? (oponent ? BKingMid : AKingMid)
                        : (oponent ? BKingEnd : AKingEnd);
                }
                if (table.Length!=0)
                {
                    totalPositionValue += table[squareIndex];
                }
                else
                {
                    Console.WriteLine("FEDE ADVERTENCIA ###########################");
                    Console.WriteLine("LINEA 129 GENERAL.CS ###########################");
                    Console.WriteLine("TABLE NO AGARRÓ NINGUNA TABLA ###########################");
                } 
                
            }

            return totalPositionValue;
        }
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