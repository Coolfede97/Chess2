using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;

namespace PositionData
{
    public static class PosData
    {
        // PAWNS ------------------------------------------------------------------
        public static readonly int[] WhitePawnEarly = 
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            5, 10, 10,-20,-20, 10, 10,  5,
            5, -5,-10,  0,  0,-10, -5,  5,
            0,  0,  0, 20, 20,  0,  0,  0,
            5,  5, 10, 25, 25, 10,  5,  5,
            10, 10, 20, 30, 30, 20, 10, 10,
            50, 50, 50, 50, 50, 50, 50, 50,
            0,  0,  0,  0,  0,  0,  0,  0
        };

        public static readonly int[] WhitePawnMid = 
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            5, 10, 10,  0,  0, 10, 10,  5,
            5, -5,-10, 10, 10,-10, -5,  5,
            0,  0,  0, 20, 20,  0,  0,  0,
            5,  5, 10, 25, 25, 10,  5,  5,
            10, 10, 20, 30, 30, 20, 10, 10,
            50, 50, 50, 50, 50, 50, 50, 50,
            0,  0,  0,  0,  0,  0,  0,  0
        };

        public static readonly int[] WhitePawnEnd = 
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            0, 10, 10, 20, 20, 10, 10,  0,
            0,  0,  0, 30, 30,  0,  0,  0,
            0,  0,  0, 40, 40,  0,  0,  0,
            0,  0,  0, 50, 50,  0,  0,  0,
            0,  0,  0, 60, 60,  0,  0,  0,
            0,  0,  0, 70, 70,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0
        };
        public static readonly int[] BlackPawnEarly = WhitePawnEarly.Reverse().ToArray();
        public static readonly int[] BlackPawnMid = WhitePawnMid.Reverse().ToArray();
        public static readonly int[] BlackPawnEnd = WhitePawnEnd.Reverse().ToArray();

        // KNIGHTS ------------------------------------------------------------------

        public static readonly int[] WhiteKnightEarly = 
        {
            -50,-40,-30,-30,-30,-30,-40,-50,
            -40,-20,  0,  0,  0,  0,-20,-40,
            -30,  0, 10, 15, 15, 10,  0,-30,
            -30,  5, 15, 20, 20, 15,  5,-30,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -30,  5, 10, 15, 15, 10,  5,-30,
            -40,-20,  0,  5,  5,  0,-20,-40,
            -50,-40,-30,-30,-30,-30,-40,-50
        };

        public static readonly int[] WhiteKnightMid = 
        {
            -50,-40,-20,-20,-20,-20,-40,-50,
            -40,-10,  5, 10, 10,  5,-10,-40,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -20, 10, 20, 25, 25, 20, 10,-20,
            -20, 10, 20, 25, 25, 20, 10,-20,
            -30,  0, 15, 20, 20, 15,  0,-30,
            -40,-10,  5, 10, 10,  5,-10,-40,
            -50,-40,-20,-20,-20,-20,-40,-50
        };
        
        public static readonly int[] WhiteKnightEnd = 
        {
            -30,-20,-10, -5, -5,-10,-20,-30,
            -20, -5, 10, 15, 15, 10, -5,-20,
            -10, 10, 20, 25, 25, 20, 10,-10,
            -5, 15, 25, 30, 30, 25, 15, -5,
            -5, 15, 25, 30, 30, 25, 15, -5,
            -10, 10, 20, 25, 25, 20, 10,-10,
            -20, -5, 10, 15, 15, 10, -5,-20,
            -30,-20,-10, -5, -5,-10,-20,-30
        };
        
        public static readonly int[] BlackKnightEarly = WhiteKnightEarly.Reverse().ToArray();
        public static readonly int[] BlackKnightMid = WhiteKnightMid.Reverse().ToArray();
        public static readonly int[] BlackKnightLate = WhiteKnightEnd.Reverse().ToArray();
        
        // BISHOPS ------------------------------------------------------------------

        private static readonly int[] WhiteBishopEarly = 
        {
            -20,-10,-10,-10,-10,-10,-10,-20,
            -10,  5,  0,  0,  0,  0,  5,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10,  0, 10, 10, 10, 10,  0,-10,
            -10,  5,  5, 10, 10,  5,  5,-10,
            -10,  0,  5, 10, 10,  5,  0,-10,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -20,-10,-10,-10,-10,-10,-10,-20
        };

        private static readonly int[] WhiteBishopMid = 
        {
            -20,-10,-10,-10,-10,-10,-10,-20,
            -10, 10,  5,  5,  5,  5, 10,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10,  5, 15, 15, 15, 15,  5,-10,
            -10,  5, 15, 20, 20, 15,  5,-10,
            -10, 10, 10, 10, 10, 10, 10,-10,
            -10, 10,  5,  5,  5,  5, 10,-10,
            -20,-10,-10,-10,-10,-10,-10,-20
        };

        private static readonly int[] WhiteBishopEnd = 
        {
            -10, -5, -5, -5, -5, -5, -5,-10,
            -5,  5,  0,  5,  5,  0,  5, -5,
            -5, 10, 10, 10, 10, 10, 10, -5,
            -5,  5, 15, 15, 15, 15,  5, -5,
            -5,  5, 15, 20, 20, 15,  5, -5,
            -5, 10, 10, 10, 10, 10, 10, -5,
            -5,  5,  0,  5,  5,  0,  5, -5,
            -10, -5, -5, -5, -5, -5, -5,-10
        };
        public static readonly int[] BlackBishopEarly = WhiteBishopEarly.Reverse().ToArray();
        public static readonly int[] BlackBishopMid = WhiteBishopMid.Reverse().ToArray();
        public static readonly int[] BlackBishopEnd = WhiteBishopEnd.Reverse().ToArray();

        // ROOKS --------------------------------------------------------------------

        private static readonly int[] WhiteRookEarly = 
        {
            0,  0,  5, 10, 10,  5,  0,  0,
            -5,  0,  0,  0,  0,  0,  0, -5,
            -5,  0,  0,  0,  0,  0,  0, -5,
            -5,  0,  0,  0,  0,  0,  0, -5,
            -5,  0,  0,  0,  0,  0,  0, -5,
            -5,  0,  0,  0,  0,  0,  0, -5,
            5, 10, 10, 10, 10, 10, 10,  5,
            0,  0,  5, 10, 10,  5,  0,  0
        };

        private static readonly int[] WhiteRookMid = 
        {
            0,  0,  5, 10, 10,  5,  0,  0,
            -5,  5, 10, 15, 15, 10,  5, -5,
            -5,  0,  5, 10, 10,  5,  0, -5,
            -5,  0,  0,  5,  5,  0,  0, -5,
            -5,  0,  0,  0,  0,  0,  0, -5,
            -5,  0,  0,  0,  0,  0,  0, -5,
            5, 10, 10, 10, 10, 10, 10,  5,
            0,  0,  5, 10, 10,  5,  0,  0
        };  

        private static readonly int[] WhiteRookEnd = 
        {
            20, 20, 20, 20, 20, 20, 20, 20,
            10, 10, 10, 10, 10, 10, 10, 10,
            5,  5,  5,  5,  5,  5,  5,  5,
            0,  0,  0,  0,  0,  0,  0,  0,
            -5, -5, -5, -5, -5, -5, -5, -5,
            -5, -5, -5, -5, -5, -5, -5, -5,
            -5, -5, -5, -5, -5, -5, -5, -5,
            0,  0,  5,  5,  5,  5,  0,  0
        };

        private static readonly int[] BlackRookEarly = WhiteRookEarly.Reverse().ToArray();
        private static readonly int[] BlackRookMid = WhiteRookMid.Reverse().ToArray();
        private static readonly int[] BlackRookEnd = WhiteRookEnd.Reverse().ToArray();

        // QUEENS -------------------------------------------------------------------
        private static readonly int[] WhiteQueenEarly = 
        {
            -20,-10,-10, -5, -5,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5,  5,  5,  5,  0,-10,
            -5,  0,  5,  5,  5,  5,  0, -5,
            0,  0,  5,  5,  5,  5,  0, -5,
            -10,  5,  5,  5,  5,  5,  0,-10,
            -10,  0,  5,  0,  0,  0,  0,-10,
            -20,-10,-10, -5, -5,-10,-10,-20
        };

        private static readonly int[] WhiteQueenMid = 
        {
            -20,-10,-10, -5, -5,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5,  5,  5,  5,  0,-10,
            -5,  0, 10, 10, 10, 10,  0, -5,
            0,  0, 10, 10, 10, 10,  0, -5,
            -10,  5,  5, 10, 10,  5,  0,-10,
            -10,  0,  5,  0,  0,  0,  0,-10,
            -20,-10,-10, -5, -5,-10,-10,-20
        };
        private static readonly int[] WhiteQueenEnd =
        {
            -20,-10,-10, -5, -5,-10,-10,-20,
            -10,  0,  0,  0,  0,  0,  0,-10,
            -10,  0,  5,  5,  5,  5,  0,-10,
            -5,  0, 10, 10, 10, 10,  0, -5,
            0,  0, 10, 10, 10, 10,  0, -5,
            -10,  5,  5, 10, 10,  5,  0,-10,
            -10,  0,  5,  0,  0,  0,  0,-10,
            -20,-10,-10, -5, -5,-10,-10,-20
        };
        private static readonly int[] BlackQueenEarly = WhiteQueenEarly.Reverse().ToArray();
        private static readonly int[] BlackQueenMid = WhiteQueenMid.Reverse().ToArray();
        private static readonly int[] BlackQueenEnd = WhiteQueenEnd.Reverse().ToArray();

        // KINGS --------------------------------------------------------------------
        private static readonly int[] WhiteKingEarly = 
        {
            -30,-40,-40,-50,-50,-40,-40,-30,
            -30,-40,-40,-50,-50,-40,-40,-30,
            -30,-40,-40,-50,-50,-40,-40,-30,
            -30,-40,-40,-50,-50,-40,-40,-30,
            -20,-30,-30,-40,-40,-30,-30,-20,
            -10,-20,-20,-20,-20,-20,-20,-10,
            20, 20,  0,   0,   0,  0, 20, 20,
            20, 30, 10,   0,   0, 10, 30, 20
        };

        private static readonly int[] WhiteKingMid = 
        {
            -50,-40,-30,-20,-20,-30,-40,-50,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -50,-40,-30,-20,-20,-30,-40,-50
        };

        private static readonly int[] WhiteKingEnd = 
        {
            -50,-40,-30,-20,-20,-30,-40,-50,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 30, 40, 40, 30,-10,-30,
            -30,-10, 20, 30, 30, 20,-10,-30,
            -30,-20,-10,  0,  0,-10,-20,-30,
            -50,-40,-30,-20,-20,-30,-40,-50
        };
        private static readonly int[] BlackKingEarly = WhiteKingEarly.Reverse().ToArray();
        private static readonly int[] BlackKingMid = WhiteKingMid.Reverse().ToArray();
        private static readonly int[] BlackKingEnd = WhiteKingEnd.Reverse().ToArray();

    }
}