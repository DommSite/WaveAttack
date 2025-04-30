using System.IO;
using System.Text.Json;

namespace WaveAttack
{
    public class GameSettings
{
    public float Volume { get; set; } = 0.8f;
    public bool Fullscreen { get; set; } = false;

    private static string settingsPath = "settings.json";

    public static GameSettings Load()
    {
        if (File.Exists(settingsPath))
        {
            var json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<GameSettings>(json);
        }
        return new GameSettings(); // Default settings
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(settingsPath, json);
    }
}
}