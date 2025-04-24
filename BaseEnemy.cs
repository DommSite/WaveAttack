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
        protected float repulsionStrength = 0.2f;
        protected Weapon weapon;


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
            ApplyRepulsion();
            if (weapon != null){
                weapon.Update(gameTime);
            }
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

        private void ApplyRepulsion()
        {
            float repulsionRadius = Math.Max(texture.Width, texture.Height) * scale * 0.6f;

            foreach (var entity in GameManager.Instance.entities)
            {
                if (entity is BaseEnemy other && other != this && other.isActive)
                {
                    Vector2 diff = position - other.position;
                    float distance = diff.Length();

                    if (distance < repulsionRadius && distance > 0.01f) // prevent divide by zero
                    {
                        Vector2 pushDir = diff / distance;
                        float pushAmount = (repulsionRadius - distance) * repulsionStrength;
                        position += pushDir * pushAmount;
                    }
                }
            }
        }

        public override void Attack(GameTime gameTime){
            if (weapon != null){
                weapon.Use(gameTime, GameManager.Instance.entities[0].position);
            }  
        }
    }
}
