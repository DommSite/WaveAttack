using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseEnemy : BaseEntity
    {
        protected Player player;
        protected float distanceFromPlayer;
        protected float wantedDistanceFromPlayer;
        public BaseEnemy(Texture2D texture, Vector2 position, int health, float speed)
        : base(texture, position, health, speed){
        }


    }
}