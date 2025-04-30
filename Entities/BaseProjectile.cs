using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaveAttack.Entities.Enemies;


namespace WaveAttack.Entities
{
    public class BaseProjectile : BaseClass
    {
        protected Vector2 direction;
        protected int damage {get; private set;}
        protected bool isEnemyProjectile {get; private set;}
        float speedProjectile = 150f;
        
        public BaseProjectile(Vector2 position, Vector2 direction, int damage, bool isEnemyProjectile):base(FileManager.GetTexture("ProjectileBullet"), position, 0.015f){
            this.direction = direction;
            this.damage = damage;
            this.isEnemyProjectile = isEnemyProjectile;
            this.direction.Normalize();
            rotation = (float)Math.Atan2(this.direction.Y, this.direction.X);
        }

        public override void Update(GameTime gameTime)
        { 
            position += direction * speedProjectile * (float)gameTime.ElapsedGameTime.TotalSeconds;
            hitBox = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
            if(isActive){
                CheckCollision();
            }          
        }

        private void CheckCollision(){
            int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            int margin = 50;
            if (position.X < 0 - (margin + texture.Width) || position.X > screenWidth + (margin + texture.Width) || position.Y < 0 - (margin + texture.Height) || position.Y > screenHeight + (margin + texture.Height))
            {
                isActive = false;
            }
            if(isEnemyProjectile){
                if(GameManager.Instance.player.hitBox.Intersects(this.hitBox)){
                    GameManager.Instance.player.TakeDamage(damage);
                    isActive = false;
                }
            }
            
            else{              
                foreach(var entity in GameManager.Instance.entities){                   
                    if(entity is BaseEnemy enemy && enemy.isActive){
                        if(enemy.hitBox.Intersects(hitBox)){
                            enemy.TakeDamage(damage);
                            isActive = false;
                            break;
                        }
                    }                          
                }
            }   
        }
        

        /*public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
        }*/
    }
}