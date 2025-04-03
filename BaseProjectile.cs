using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WaveAttack
{
    public class BaseProjectile : BaseClass
    {
        protected Vector2 direction;
        protected int damage {get; private set;}
        protected bool isEnemyProjectile {get; private set;}
        float speedProjectile = 10f;
        protected float rotation;

        public BaseProjectile(Vector2 position, Vector2 direction, int damage, bool isEnemyProjectile):base(FileManager.GetTexture("ProjectileBullet"), position){
            this.direction = direction;
            this.damage = damage;
            this.isEnemyProjectile = isEnemyProjectile;
            this.direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        public override void Update(GameTime gameTime)
        {
            position += direction * speedProjectile * (float)gameTime.ElapsedGameTime.TotalSeconds;;

            if (position.X < 0 || position.X > 800 || position.Y < 0 || position.Y > 600)
            {
                isActive = false;
            }

            CheckCollision();
        }

        private void CheckCollision(){
            if(isEnemyProjectile){
                if(GameManager.Instance.player.hitBox.Intersects(hitBox)){
                    GameManager.Instance.player.TakeDamage(damage);
                    isActive = false;
                }
            }

            foreach(var enemy in GameManager.Instance.enemies){
                if(enemy.hitBox.Intersects(hitBox)){
                    enemy.TakeDamage(damage);
                    isActive = false;
                    break;
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            }
        }
    }
}