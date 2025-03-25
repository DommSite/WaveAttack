using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseEnemy : BaseEntity
    {
        public BaseEnemy(Texture2D texture, Vector2 position, int health, float speed)
        : base(texture, position, health, speed)
    {
    }

    public abstract void Attack(Player player);
    }
}