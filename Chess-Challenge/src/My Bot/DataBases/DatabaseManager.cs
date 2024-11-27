using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using ChessChallenge.API;
using System;
using System.Text.Json;
public static class DatabaseManager
{
    private static string openingsPath = "src/My Bot/DataBases/Openings.json";
    
    public static List<FenMove> LoadFenMoveList()
    {
        if (!File.Exists(openingsPath))
        {
            Console.WriteLine("No se encuentra el archivo Openings.json :(");
        }
        string jsonContent = File.ReadAllText(openingsPath);
        List<FenMove>? fenMoves = JsonSerializer.Deserialize<List<FenMove>>(jsonContent);
        return fenMoves ?? new List<FenMove>();
        // foreach (FenMove fenMove in data)
        // {
        //     Console.WriteLine(fenMove.fen);
        //     Console.WriteLine(fenMove.materialWon);
        // }
        
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