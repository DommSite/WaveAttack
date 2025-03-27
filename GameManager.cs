using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace WaveAttack
{
    public class GameManager
    {
        private static GameManager? _instance;
        public static GameManager Instance => _instance ??= new GameManager();
        private List<BaseProjectile> projectiles = new();
        private List<BaseEnemy> enemies = new();
        public Player player { get; }

        private GameManager() { }

        public Texture2D swordTexture;
        public Texture2D bigSwordTexture;
        public Texture2D gunTexture;
        public Texture2D bulletTexture;

        public void Initialize(Game1 game)
    {
        var playerTexture = game.Content.Load<Texture2D>("player");
        player = new Player(playerTexture, new Vector2(400, 300));
    }














    }
}