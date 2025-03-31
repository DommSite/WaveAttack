using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class EnemyProjectile : BaseProjectile
    {
        public EnemyProjectile(Vector2 position, Vector2 direction, int damage)
        : base(SpriteManager.GetTexture("ProjectileBullet"), position, direction, damage, isEnemyProjectile: true) { }
    }
}