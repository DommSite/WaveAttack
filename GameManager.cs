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


        public void Initialize(Game1 game)
        {
            SpriteManager.LoadContent(game.Content);
            var playerTexture = game.Content.Load<Texture2D>("player");
            player = new Player(playerTexture, new Vector2(400, 300));
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (!enemies[i].IsActive){
                    enemies.RemoveAt(i);
                }              
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);
                if (!projectiles[i].IsActive){
                    projectiles.RemoveAt(i);
                }               
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            foreach (var enemy in enemies){
                enemy.Draw(spriteBatch);
            }
                
            foreach (var projectile in projectiles){
                projectile.Draw(spriteBatch);
            }      
        }




        
    }
}