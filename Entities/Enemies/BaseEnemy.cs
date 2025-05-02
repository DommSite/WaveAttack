using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack.Entities.Enemies
{
    public abstract class BaseEnemy : BaseEntity
    {
        public Player player;
        protected float distanceFromPlayer;
        protected float wantedDistanceFromPlayer;
        protected TimeSpan attackCooldown;
        protected TimeSpan lastAttackTime = TimeSpan.Zero;
        protected int points;
        



        public BaseEnemy(Texture2D texture, Vector2 position, float scale, int health, float speed, double attackCooldown, float wantedDistanceFromPlayer, int points)
        : base(texture, position, scale, health, speed, 0.3f){
            this.attackCooldown = TimeSpan.FromSeconds(attackCooldown);
            this.wantedDistanceFromPlayer = wantedDistanceFromPlayer;
            this.points = points;
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
                if(!weapon.isAttacking){
                    position += direction * speed;
                }           
            }
        }


        public override void Die(){
            if(isActive){
                GameManager.Instance.player.KillEnemy(points);
                GameManager.Instance.player.killCount++;
                System.Console.WriteLine("Current killcount: " + GameManager.Instance.player.killCount);
                isActive = false;
            }  
        }

        

        public override void Attack(GameTime gameTime){
            if (weapon != null){
                weapon.Use(gameTime, GameManager.Instance.entities[0].position);
            }  
        }
    }
}
