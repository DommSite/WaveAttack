using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;

public class GameSettings
{
    public float Volume { get; set; } = 1f;
    public float SFXVolume { get; set; } = 1f;
    public bool Fullscreen { get; set; } = false;
    public bool MuteAll { get; set; } = false;
    public Point Resolution { get; set; } = new Point(1280, 720); // Default
    public int totalRounds { get; set; } = 3; // Default to 3
    private static string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

    public void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(FilePath, json);
    }

    public static GameSettings Load()
    {
        if (!File.Exists(FilePath))
            return new GameSettings();

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<GameSettings>(json);
    }
    public void Apply(GraphicsDeviceManager graphics)
    {
        graphics.IsFullScreen = Fullscreen;
        graphics.PreferredBackBufferWidth = Resolution.X;
        graphics.PreferredBackBufferHeight = Resolution.Y;
        graphics.ApplyChanges();
    }
}