using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseClass{
        public Vector2   position{get; set;}
        protected Texture2D texture;
        
        public bool isActive{get; set;} = true;
        public Rectangle hitBox => new((int)position.X, (int)position.Y, texture.Width, texture.Height);       

        public BaseClass(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }       
}