using System.Text.Json;
using System.IO;
using System.Collections.Generic;

public static class DatabaseManager
{
    private static string openingsPath = "Openings.json";
    
    public static void SaveOpeningData(List<openingData> data)
    {
        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions{WriteIndented=true});
        File.WriteAllText(openingsPath, json);
    }

    public class openingData
    {
        public string fen {get; set;}
        public string bestMove {get;set;}
        public int materialWon {get;set;}
    }

}