using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

public class LeaderboardEntry
{
    public string Name { get; set; }
    public int Score { get; set; }

    // File path for saving/loading the leaderboard data
    private static readonly string filePath = "leaderboard.json";
    
    // Constructor
    public LeaderboardEntry() { }

    public LeaderboardEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }

    // Add an entry, sort the leaderboard, and save it
    public static void AddEntry(LeaderboardEntry entry)
    {
        // Load existing leaderboard entries
        var entries = LoadList();

        // Add the new entry to the leaderboard
        entries.Add(entry);

        // Sort the entries by score in descending order
        entries = entries.OrderByDescending(e => e.Score).ToList();

        // Keep only the top 10 entries
        if (entries.Count > 10)
        {
            entries = entries.Take(10).ToList();
        }

        // Save the updated leaderboard
        SaveList(entries);
    }

    // Save the leaderboard to a file
    private static void SaveList(List<LeaderboardEntry> entries)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(entries, options);
        File.WriteAllText(filePath, json);
    }

    // Load the leaderboard from a file
    public static List<LeaderboardEntry> LoadList()
    {
        if (!File.Exists(filePath))
            return new List<LeaderboardEntry>();

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<LeaderboardEntry>>(json) ?? new List<LeaderboardEntry>();
    }

    public static bool IsViableEntry(int score){
        if(LoadList().Count < 10){
            return true;
        }
        else if(score > LoadList()[LoadList().Count - 1].Score){
            return true;
        }
        else{
            return false;
        }
    }
}