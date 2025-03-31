using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class PlayerProjectile : BaseProjectile
    {
        public PlayerProjectile(Vector2 position, Vector2 direction, int damage)
        : base(SpriteManager.GetTexture("ProjectileBullet"), position, direction, damage, isEnemyProjectile: false) { }
    }
}