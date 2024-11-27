using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;
using static General.Gen;
using static DatabaseManager;
public class MyBot : IChessBot
{
    public int moveCount=0;
    public bool openingSaved=false;
    public List<FenMove> openingHistory = new List<FenMove>();
    public Move Think(Board board, Timer timer)
    {
        Brokenice bot = new Brokenice(board.IsWhiteToMove);
        Candidate move = bot.Think(board,timer);
        if (!openingSaved && moveCount>=6 && GetNoisyMoves(board,board.IsWhiteToMove).Count==0) UpdateOpeningsDatabase(move, board);
        else if (!openingSaved) AddNewFenMove(board, move);
        moveCount++;
        return  move.movement;
    }

    public void UpdateOpeningsDatabase(Candidate move, Board board)
    {
        openingSaved=true;
        if (MaterialDifference(board.IsWhiteToMove, board, 0)<0) return;
        List<FenMove> OpeningsList = LoadFenMoveList();
        foreach (FenMove fenMove in openingHistory)
        {
            fenMove.MaterialWon=move.materialWon;
            
                FenMove? savedFenMove = OpeningsList.Find(x=>x.Fen==fenMove.Fen);
                if (savedFenMove==null)
                {
                    OpeningsList.Add(fenMove);
                }
                else if (fenMove.MaterialWon>savedFenMove.MaterialWon)
                {
                    Console.WriteLine($"Se cambio del movimiento {savedFenMove.BestMove.ToString()} al movimiento {fenMove.BestMove} en el FEN: {fenMove.Fen}");
                    savedFenMove.BestMove=fenMove.BestMove;
                    savedFenMove.MaterialWon=fenMove.MaterialWon;
                }
        }
        SaveFenMoveList(OpeningsList);
    }
    public void AddNewFenMove(Board board, Candidate move)
    {
        FenMove newFenMove = new FenMove(board.GetFenString(),move.movement,move.materialWon);
        openingHistory.Add(newFenMove);
    }
}
