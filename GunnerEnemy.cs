using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class GunnerEnemy : BaseEnemy
    {
        public GunnerEnemy(Vector2 position) : base(FileManager.GetTexture("EnemySniper"), position, 0.03f, 30, 1f, 2, 150)
        {

        }


        public override void Attack(GameTime gameTime){
            Vector2 bulletDirection = Vector2.Normalize(player.position - position);
                //GameManager.Instance.SpawnProjectile(new EnemyProjectile(bulletTexture, position, bulletDirection));              
            
        }





    }
}