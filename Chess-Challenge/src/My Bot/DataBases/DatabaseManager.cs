using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using ChessChallenge.API;
public static class DatabaseManager
{
    private static string openingsPath = "Openings.json";
    
    public static void SaveOpeningData(List<fenMove> data)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented=true});
        File.WriteAllText(openingsPath, json);
    }

    public class fenMove
    {
        public string fen {get; set;}
        public Move bestMove {get;set;}
        public int materialWon {get;set;}

        public fenMove(string fenP, Move bestMoveP, int materialWonP)
        {
            fen=fenP;
            bestMove=bestMoveP;
            materialWon=materialWonP;
        }
    }

}