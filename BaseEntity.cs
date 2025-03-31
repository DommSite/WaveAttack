using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseEntity : BaseClass
    {
        public int health{get; protected set;}
        protected float speed;

        public BaseEntity(Texture2D texture, Vector2 position, int health, float speed) : base(texture, position){
            this.health = health;
            this.speed = speed;
        }

        public void TakeDamage(int damage){
            health -= damage;
            if(health <= 0){
                health = 0;
                Die();
            }
        }
        public abstract void Die();

        public abstract void Move(GameTime gameTime);
        public abstract void Attack(GameTime gameTime);

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}