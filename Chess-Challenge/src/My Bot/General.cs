using System;
using System.Collections.Generic;
using System.Linq;
using ChessChallenge.API;

public class General
{

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
}