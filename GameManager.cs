using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WaveAttack.Entities;
using WaveAttack.Entities.Enemies;
using WaveAttack;


namespace WaveAttack
{
    public class GameManager
    {
        
        private static GameManager _instance;
        public static GameManager Instance => _instance ?? (_instance = new GameManager());
        public Player player { get; private set;}
        public List<BaseClass> entities = new();
        
        private GameManager() { }
        private HUD hud;
        private Random random = new Random();
        private TimeSpan spawnTimer = TimeSpan.Zero;
        private TimeSpan spawnInterval = TimeSpan.FromSeconds(0.25);
        public float MasterVolume = 1;
        private GameState currentState = GameState.MainMenu;
        private MenuManager menuManager;
        private KeyboardState previousKeyboardState;
        private GraphicsDevice graphicsDevice;




        public void Initialize(Game1 game, GraphicsDevice graphicsDevice)
        { 
            player = new Player(new Vector2(400, 300));
            entities.Add(player);
            
            hud = new HUD(player, graphicsDevice);
            menuManager = new MenuManager(graphicsDevice);
            this.graphicsDevice = graphicsDevice;
        }

        public void LoadContent(Game1 game){
            FileManager.LoadContent(game.Content);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (currentState == GameState.Playing)
            {
                if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                {
                    ChangeState(GameState.Paused);
                }

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
                player.weapon.Update(gameTime);
            }
            else if(currentState == GameState.Paused){
                if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
                    {
                        ChangeState(GameState.Playing);
                    }
                    menuManager.Update(currentState);
            }
            else
            {
                menuManager.Update(currentState);
            }
            previousKeyboardState = keyboardState;
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (currentState == GameState.Playing)
            {
                foreach (var entity in entities){
                    if(entity is not Player){
                        entity.Draw(spriteBatch, gameTime);
                        if(entity is BaseEntity baseEntity){
                            baseEntity.weapon?.Draw(spriteBatch, gameTime);
                        }   
                    }
                } 
                player.Draw(spriteBatch, gameTime);
                player.weapon?.Draw(spriteBatch,gameTime);
                hud.Draw(spriteBatch);
            }
            else
            {
                menuManager.Draw(spriteBatch, currentState);
            }


            

            
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
            else if(68  <= type && type <= 10){
                enemy = new ChunkyEnemy(GetRandomSpawnPosition());
            }
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


        /*public Vector2 RotatePoint(Vector2 point, Vector2 center, float angle)
        {
            float cosAngle = (float)Math.Cos(angle);
            float sinAngle = (float)Math.Sin(angle);
   
            point -= center;
            
            float newX = point.X * cosAngle - point.Y * sinAngle;
            float newY = point.X * sinAngle + point.Y * cosAngle;

            return new Vector2(newX + center.X, newY + center.Y);
        }*/


        public bool IsPointInsidePolygon(Rectangle entityHitBox, Vector2[] polygon)
        {
            int intersections = 0;
           
            Vector2 point = new Vector2(entityHitBox.X + entityHitBox.Width / 2, entityHitBox.Y + entityHitBox.Height / 2);
          
            for (int i = 0; i < polygon.Length; i++)
            {
                Vector2 vertex1 = polygon[i];
                Vector2 vertex2 = polygon[(i + 1) % polygon.Length];

                if (DoIntersect(point, vertex1, vertex2))
                {
                    intersections++;
                }
            }

            
            return (intersections % 2 == 1);
        }


        private bool DoIntersect(Vector2 point, Vector2 vertex1, Vector2 vertex2)
        {
            
            if (vertex1.Y > vertex2.Y)
            {
                Vector2 temp = vertex1;
                vertex1 = vertex2;
                vertex2 = temp;
            }

            
            if (point.Y == vertex1.Y || point.Y == vertex2.Y)
                point.Y += 0.1f;  

            if (point.Y > vertex2.Y || point.Y < vertex1.Y)
                return false;

            float xIntersection = (point.Y - vertex1.Y) * (vertex2.X - vertex1.X) / (vertex2.Y - vertex1.Y) + vertex1.X;

            
            return point.X < xIntersection;
        } 

        public void ChangeState(GameState newState)
        {
            currentState = newState;
        }

        public void Exit(){
            Game1.Instance.Exit();
        }

    public void ResetGame(){
        entities.Clear();  // Remove all existing entities
        player = new Player(new Vector2(400, 300)); // Respawn player
        entities.Add(player);

        hud = new HUD(player, graphicsDevice); // Recreate HUD
    }





    }
}