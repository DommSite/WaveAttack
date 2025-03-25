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
        }

        public override void Update(GameTime gameTime){
            timeSinceLastShot += gameTime.ElapsedGameTime;
            Attack();
            Move();
        }

        private void Attack(){
            
        if (timeSinceLastShot >= fireCooldown)
        {
            Vector2 bulletDirection = Vector2.Normalize(player.Position - position);
            GameManager.Instance.SpawnProjectile(new EnemyProjectile(bulletTexture, position, bulletDirection));
            timeSinceLastShot = TimeSpan.Zero;
        }
    }
        }






    }
}