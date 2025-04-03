using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class StandardEnemy : BaseEnemy
    {
        private TimeSpan attackCooldown = TimeSpan.FromSeconds(1);
        private TimeSpan lastAttackTime = TimeSpan.Zero;
        

        public StandardEnemy(Vector2 position, Player player)
            : base(FileManager.GetTexture("EnemyStandard"), position, 50, 2f)
        {
            this.player = player;
            wantedDistanceFromPlayer = 15;
        }

        public override void Update(GameTime gameTime){
            distanceFromPlayer = Vector2.Distance(position, player.position);
            lastAttackTime += gameTime.ElapsedGameTime;
            Move(gameTime);
            Attack(gameTime);            
        }

        public override void Attack(GameTime gameTime){
            if((lastAttackTime >= attackCooldown) && (distanceFromPlayer <= wantedDistanceFromPlayer)){
                lastAttackTime = TimeSpan.Zero;
                //code do code
            }            
        }

        public override void Move(GameTime gameTime)
        {
            Vector2 direction = player.position - position;
            
            if ((direction != Vector2.Zero) && (distanceFromPlayer >= wantedDistanceFromPlayer)){
                direction.Normalize();           
                position += direction * speed;
            }
        }

        /*public override void Die(){
        }*/
    }
}