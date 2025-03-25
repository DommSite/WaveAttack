using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseProjectile : BaseClass
    {
        protected Vector2 velocity;
        protected int damage {get; private set;}
        protected bool isEnemyProjectile {get; private set;}
        float speedProjectile = 10f;
        public BaseProjectile(Texture2D texture, Vector2 position, Vector2 direction, int damage, bool isEnemyProjectile):base(texture, position){
            this.velocity = direction * speedProjectile;
            this.damage = damage;
            this.isEnemyProjectile = isEnemyProjectile;
        }

        public override void Update(GameTime gameTime)
        {
            position += velocity;

            if (position.X < 0 || position.X > 800 || position.Y < 0 || position.Y > 600)
            {
                isActive = false;
            }
        }
    }
}