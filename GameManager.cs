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
        //private List<BaseProjectile> projectiles = new();
        //public List<BaseEnemy> enemies = new();
        public Player player { get; private set;}
        public List<BaseClass> entities = new();
        
        private GameManager() { }
        private HUD hud;
        private Random random = new Random();
        private TimeSpan spawnTimer = TimeSpan.Zero;
        private TimeSpan spawnInterval = TimeSpan.FromSeconds(3);


        public void Initialize(Game1 game, GraphicsDevice graphicsDevice)
        { 
            player = new Player(new Vector2(400, 300));
            entities.Add(player);
            
            hud = new HUD(player, graphicsDevice);
        }

        public void LoadContent(Game1 game){
            FileManager.LoadContent(game.Content);
        }

        public void Update(GameTime gameTime)
        {
            spawnTimer += gameTime.ElapsedGameTime;
            
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                entities[i].Update(gameTime);
                if (!entities[i].isActive){
                    if(entities[i] is BaseEntity){
                        ((BaseEntity)entities[i]).Die();
                    }
                    entities.RemoveAt(i);
                }
            }

            if (spawnTimer >= spawnInterval)
            {
                SpawnRandomEnemy();
                spawnTimer = TimeSpan.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities){
                entity.Draw(spriteBatch);
            } 

            hud.Draw(spriteBatch);
        }


        public void SpawnRandomEnemy(){
            int type = random.Next(0,100);

            BaseEnemy enemy;

            if(0  <= type && type <= 33){
                enemy = new StandardEnemy(GetRandomSpawnPosition());
            }
            else if(34  <= type && type <= 67){
                enemy = new GunnerEnemy(GetRandomSpawnPosition());
            }
            /*else if(68  <= type && type <= 10){
                enemy = new ChunkyEnemy(GetRandomSpawnPosition());
            }*/
            else{
                enemy = new StandardEnemy(GetRandomSpawnPosition());
            }

            enemy.player = this.player;

            entities.Add(enemy);
        }

        public void AddProjectile(BaseProjectile projectile){
            entities.Add(projectile);
        }

        private Vector2 GetRandomSpawnPosition(){
            Vector2 spawnPos;
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int margin = 50;
            int safeRadius = 200;
            int attempts = 0;
            int maxAttempts = 10;

            do{
                int side = random.Next(0,4);

                spawnPos = side switch
                {
                    0 => new Vector2(-margin, random.Next(0, screenHeight)), 
                    1 => new Vector2(screenWidth + margin, random.Next(0, screenHeight)), 
                    2 => new Vector2(random.Next(0, screenWidth), -margin), 
                    _ => new Vector2(random.Next(0, screenWidth), screenHeight + margin), 
                };
                
                attempts++;

            }while(Vector2.Distance(spawnPos, player.position) < safeRadius && attempts < maxAttempts);

            return spawnPos;
        }       
    }
}