using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class PlayerProjectile : BaseProjectile
    {
        public PlayerProjectile(Texture2D texture, Vector2 position, Vector2 direction, int damage)
        : base(texture, position, direction, damage, isEnemyProjectile: false) { }
    }
}