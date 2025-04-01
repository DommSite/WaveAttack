using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace WaveAttack
{
    public class GameManager
    {
        
        private static GameManager _instance;
        public static GameManager Instance => _instance ?? (_instance = new GameManager());
        private List<BaseProjectile> projectiles = new();
        public List<BaseEnemy> enemies = new();
        public Player player { get; private set;}
        private GameManager() { }


        public void Initialize(Game1 game)
        {
            SpriteManager.LoadContent(game.Content);
            player = new Player(new Vector2(400, 300));
        }

        public void LoadContent(){
            

        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);
                if (!enemies[i].isActive){
                    enemies.RemoveAt(i);
                }              
            }

            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update(gameTime);
                if (!projectiles[i].isActive){
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