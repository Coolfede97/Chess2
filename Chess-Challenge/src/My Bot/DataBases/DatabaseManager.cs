using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using ChessChallenge.API;
using System;
public static class DatabaseManager
{
    public static Random random = new Random();
    private static string[] ECOs = 
    {
        "src/My Bot/DataBases/ecoA.json",
        "src/My Bot/DataBases/ecoB.json",
        "src/My Bot/DataBases/ecoC.json",
        "src/My Bot/DataBases/ecoD.json",
        "src/My Bot/DataBases/ecoE.json"
    };

    public static Dictionary<string, Opening> openings;
    public static Dictionary<string, Opening> LoadECOS()
{
    Dictionary<string, Opening> openings = new();
    foreach (string ecoPath in ECOs)
    {
        if (!File.Exists(ecoPath))
        {
            Console.WriteLine($"No se encuentra el archivo: {ecoPath}");
            continue;
        }

        string jsonContent = File.ReadAllText(ecoPath);

        // Cambiamos la deserialización a Dictionary<string, Opening>
        var ECO = JsonSerializer.Deserialize<Dictionary<string, Opening>>(jsonContent);

        if (ECO != null)
        {
            foreach (var entry in ECO)
            {
                openings[entry.Key] = entry.Value; // Combina las aperturas
            }
        }
    }
    return openings;
}
    public class Opening
    {
        public string Src { get; set; }       
        public string Eco { get; set; }        
        public string Moves { get; set; }      
        public string Name { get; set; }       
        public string Scid { get; set; }       
        public Aliases Aliases { get; set; }   
        public Opening(string src, string eco, string moves, string name, string scid, Aliases aliases)
        {   
            Src = src;
            Eco = eco;
            Moves = moves;
            Name = name;
            Scid = scid;
            Aliases = aliases;
        }
    }

    public class Aliases
    {
        public string Scid { get; set; }       // Alias SCID
        public string EcoWikip { get; set; }   // Información adicional de ECO en Wikipedia

        public Aliases(string scid, string ecoWikip)
        {
            Scid = scid;
            EcoWikip = ecoWikip;
        }
    }

    public class FenMove
    {
        public string Fen { get; set; } // La posición FEN
        public string BestMove { get; set; } // El mejor movimiento
        public int MaterialWon { get; set; } // Material ganado

        // Constructor vacío (obligatorio para deserializar sin errores)
        public FenMove() { }

        // Constructor adicional para inicializar
        public FenMove(string fen, Move bestMove, int materialWon)
        {
            Fen = fen;
            BestMove = bestMove.ToString();
            MaterialWon = materialWon;
        }
    }

}