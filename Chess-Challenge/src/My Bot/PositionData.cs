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
        public static readonly int[] APawnEarly = 
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

        public static readonly int[] APawnMid = 
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

        public static readonly int[] APawnEnd = 
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
        public static readonly int[] BPawnEarly = APawnEarly.Reverse().ToArray();
        public static readonly int[] BPawnMid = APawnMid.Reverse().ToArray();
        public static readonly int[] BPawnEnd = APawnEnd.Reverse().ToArray();

        // KNIGHTS ------------------------------------------------------------------

        public static readonly int[] AKnightEarly = 
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

        public static readonly int[] AKnightMid = 
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
        
        public static readonly int[] AKnightEnd = 
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
        
        public static readonly int[] BKnightEarly = AKnightEarly.Reverse().ToArray();
        public static readonly int[] BKnightMid = AKnightMid.Reverse().ToArray();
        public static readonly int[] BKnightLate = AKnightEnd.Reverse().ToArray();
        
        // BISHOPS ------------------------------------------------------------------

        public static readonly int[] ABishopEarly = 
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

        public static readonly int[] ABishopMid = 
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

        public static readonly int[] ABishopEnd = 
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
        public static readonly int[] BBishopEarly = ABishopEarly.Reverse().ToArray();
        public static readonly int[] BBishopMid = ABishopMid.Reverse().ToArray();
        public static readonly int[] BBishopEnd = ABishopEnd.Reverse().ToArray();

        // ROOKS --------------------------------------------------------------------

        public static readonly int[] ARookEarly = 
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

        public static readonly int[] ARookMid = 
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

        public static readonly int[] ARookEnd = 
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

        public static readonly int[] BRookEarly = ARookEarly.Reverse().ToArray();
        public static readonly int[] BRookMid = ARookMid.Reverse().ToArray();
        public static readonly int[] BRookEnd = ARookEnd.Reverse().ToArray();

        // QUEENS -------------------------------------------------------------------
        public static readonly int[] AQueenEarly = 
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

        public static readonly int[] AQueenMid = 
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
        public static readonly int[] AQueenEnd =
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
        public static readonly int[] BQueenEarly = AQueenEarly.Reverse().ToArray();
        public static readonly int[] BQueenMid = AQueenMid.Reverse().ToArray();
        public static readonly int[] BQueenEnd = AQueenEnd.Reverse().ToArray();

        // KINGS --------------------------------------------------------------------
        public static readonly int[] AKingEarly = 
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

        public static readonly int[] AKingMid = 
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

        public static readonly int[] AKingEnd = 
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
        public static readonly int[] BKingEarly = AKingEarly.Reverse().ToArray();
        public static readonly int[] BKingMid = AKingMid.Reverse().ToArray();
        public static readonly int[] BKingEnd = AKingEnd.Reverse().ToArray();

    }
}