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
                RatingPlus+= isWhite!=board.IsWhiteToMove ? 15 : -15;
            }
            if (board.IsInCheck())
            {
                RatingPlus+= isWhite!=board.IsWhiteToMove ? 30 : -30;
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
                int squareIndex = piece.Square.Index;

                int[] table = { };

                if (piece.PieceType == PieceType.Pawn)
                {
                    table = gamePhase == 2 ? (oponent ? BPawnEnd : APawnEnd)
                                        : (oponent ? BPawnMid : APawnMid);
                }
                else if (piece.PieceType == PieceType.Knight)
                {
                    table = gamePhase == 2 ? (oponent ? BKnightLate : AKnightEnd)
                                        : (oponent ? BKnightMid : AKnightMid);
                }
                else if (piece.PieceType == PieceType.Bishop)
                {
                    table = gamePhase == 2 ? (oponent ? BBishopEnd : ABishopEnd)
                                        : (oponent ? BBishopMid : ABishopMid);
                }
                else if (piece.PieceType == PieceType.Rook)
                {
                    table = gamePhase == 2 ? (oponent ? BRookEnd : ARookEnd)
                                        : (oponent ? BRookMid : ARookMid);
                }
                else if (piece.PieceType == PieceType.Queen)
                {
                    table = gamePhase == 2 ? (oponent ? BQueenEnd : AQueenEnd)
                                        : (oponent ? BQueenMid : AQueenMid);
                }
                else if (piece.PieceType == PieceType.King)
                {
                    table = gamePhase == 2 ? (oponent ? BKingEnd : AKingEnd)
                                        : (oponent ? BKingMid : AKingMid);
                }
                if (table.Length != 0)
                {
                    totalPositionValue += table[squareIndex];
                }
                else
                {
                    Console.WriteLine("ADVERTENCIA: No se encontró una tabla válida para la pieza en la casilla " + squareIndex);
                }
            }

            return totalPositionValue;
        }

        public static List<Move> GetNoisyMoves(Board board, bool isWhite)
        {
            Move[] legalMoves = board.GetLegalMoves();
            List<Move> noisyMoves = new List<Move>();
            foreach (Move legalMove in legalMoves)
            {
                if (legalMove.IsCapture || legalMove.IsPromotion) noisyMoves.Add(legalMove);
                else
                {
                    board.MakeMove(legalMove);
                    if (isWhite!=board.IsWhiteToMove && (board.IsInCheck() || board.IsInCheckmate())) noisyMoves.Add(legalMove);
                    board.UndoMove(legalMove);
                }
            }
            return noisyMoves;
        }

        public static List<Move> FilterNoisyMoves(Board board, bool isWhite, List<Move> moves)
        {
            List<Move> noisyMoves = new List<Move>();
            foreach (Move legalMove in moves)
            {
                if (legalMove.IsCapture || legalMove.IsPromotion) noisyMoves.Add(legalMove);
                else
                {
                    board.MakeMove(legalMove);
                    if (isWhite!=board.IsWhiteToMove && (board.IsInCheck() || board.IsInCheckmate())) noisyMoves.Add(legalMove);
                    board.UndoMove(legalMove);
                }
            }
            return noisyMoves;
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

        public static Move[] CandidatesToArray(List<Candidate> candidates)
        {
            List<Move> moveList = new List<Move>();
            foreach (Candidate candidate in candidates)
            {
                moveList.Add(candidate.movement);
            }
            Move[] moveArray = moveList.ToArray();
            return moveArray;
        }
       
        public static List<Candidate> SortByMaterialDescending(List<Candidate> candidates)
        {
            return candidates.OrderByDescending(c => c.materialWon).ToList();
        }   

        public static List<Candidate> SortByMaterialAscending(List<Candidate> candidates)
        {
            return candidates.OrderBy(c => c.materialWon).ToList();
        }
        public static bool GameIsFinished(Board board)
        {
            if (board.IsInCheckmate() || board.IsInStalemate() || board.IsInsufficientMaterial() || board.IsFiftyMoveDraw())
            {
                return true;
            }
            else return false;
        }

        // public static string MoveToString(string input)
        // {
            
        // }
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