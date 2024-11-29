using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
using static DatabaseManager;
public class MyBot : IChessBot
{
    public bool openingFinished=false;
    public int moveCount=0;

    public Random random = new Random();
    public Move Think(Board board, Timer timer)
    {
        Candidate move;
        if (!openingFinished)
        {
            move = GetBookMove(board);
            if (move.movement!=Move.NullMove) return move.movement; 
        }
        Brokenice bot = new Brokenice(board.IsWhiteToMove);
        move = bot.Think(board,timer);
        
        return  move.movement;
    }
    public Candidate GetBookMove(Board board)
    {
        Move[] legalMoves = board.GetLegalMoves();
        List<Move> bookMoves= new List<Move>();
        foreach (Move legalMove in legalMoves)
        {
            board.MakeMove(legalMove);
            string fen = board.GetFenString();
            if (openings.TryGetValue(fen,out var opening)) bookMoves.Add(legalMove);
            board.UndoMove(legalMove);
        }
        if (bookMoves.Count<=0)
        {
            openingFinished=true;
            string fen = board.GetFenString();
            Console.WriteLine($"No se encontró este opening en la base de datos: {fen}");
            return new Candidate(Move.NullMove,0);
        }
        else
        {
            int randomIndex = random.Next(0,bookMoves.Count);
            return new Candidate(bookMoves[randomIndex],0);
        }
    }
}
