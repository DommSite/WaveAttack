using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaveAttack.Weapons;

namespace WaveAttack.Entities.Enemies{
    public class GunnerEnemy : BaseEnemy
    {
        public GunnerEnemy(Vector2 position) : base(FileManager.GetTexture("EnemySniper"), position, 0.03f, 30, 1f, 2, 150)
        {
            weapon = new Flintlock(this);
        }


        /*public override void Attack(GameTime gameTime){
            Vector2 bulletDirection = Vector2.Normalize(player.position - position);
                //GameManager.Instance.SpawnProjectile(new EnemyProjectile(bulletTexture, position, bulletDirection));              
            
        }*/





    }
}