using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;

public class MyBot : IChessBot
{
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
                List<Piece> piecesA=General.GetAllPieces(board,IamWhite==true);
                List<Piece> piecesB=General.GetAllPieces(board,!IamWhite==true);
                int materialWonOnMove = MaterialDifference(piecesA, piecesB);
                Console.WriteLine(materialWonOnMove);
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
                board.UndoMove(move);
            }
            int randomIndex = random.Next(0,candidates.Count);
            candidate = candidates[randomIndex];
            return candidate.movement;
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
