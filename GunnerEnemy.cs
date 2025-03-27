using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class GunnerEnemy : BaseEnemy
    {
        private Texture2D bulletTexture;
        private TimeSpan fireCooldown = TimeSpan.FromSeconds(2);
        private TimeSpan timeSinceLastShot = TimeSpan.Zero;

        public GunnerEnemy(Texture2D texture, Texture2D bulletTexture, Vector2 position) : base(texture, position, 30, 1f)
        {
            this.bulletTexture = bulletTexture;
            wantedDistanceFromPlayer = 150;
        }

        public override void Update(GameTime gameTime){
            distanceFromPlayer = Vector2.Distance(position, player.position);
            timeSinceLastShot += gameTime.ElapsedGameTime;
            Attack(gameTime);
            Move(gameTime);
        }

        public override void Move(GameTime gameTime)
        {
            Vector2 direction = player.position - position;
            
            if ((direction != Vector2.Zero) && (distanceFromPlayer >= wantedDistanceFromPlayer)){
                direction.Normalize();           
                position += direction * speed;
            }


        }

        public override void Attack(GameTime gameTime){
            
            if ((timeSinceLastShot >= fireCooldown) && (distanceFromPlayer <= wantedDistanceFromPlayer))
            {
                timeSinceLastShot = TimeSpan.Zero;
                Vector2 bulletDirection = Vector2.Normalize(player.position - position);
                GameManager.Instance.SpawnProjectile(new EnemyProjectile(bulletTexture, position, bulletDirection));              
            }
        }
    }
}