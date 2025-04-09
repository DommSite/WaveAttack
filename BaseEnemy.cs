using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseEnemy : BaseEntity
    {
        public Player player;
        protected float distanceFromPlayer;
        protected float wantedDistanceFromPlayer;
        protected TimeSpan attackCooldown;
        protected TimeSpan lastAttackTime = TimeSpan.Zero;

        public BaseEnemy(Texture2D texture, Vector2 position, float scale, int health, float speed, double attackCooldown, float wantedDistanceFromPlayer)
        : base(texture, position, scale, health, speed){
            this.attackCooldown = TimeSpan.FromSeconds(attackCooldown);
            this.wantedDistanceFromPlayer = wantedDistanceFromPlayer;
        }

        public override void Update(GameTime gameTime){       
            lastAttackTime += gameTime.ElapsedGameTime;
            if((lastAttackTime >= attackCooldown) && (distanceFromPlayer <= wantedDistanceFromPlayer)){
                lastAttackTime = TimeSpan.Zero;
                Attack(gameTime);
            }     
            Move(gameTime);
            hitBox = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public override void Move(GameTime gameTime)
        {
            Vector2 direction = player.position - position;
            distanceFromPlayer = Vector2.Distance(position, player.position);
            
            if ((direction != Vector2.Zero) && (distanceFromPlayer >= wantedDistanceFromPlayer)){
                direction.Normalize();           
                position += direction * speed;
            }
        }


        public override void Die(){
            isActive = false;
        }


    }
}