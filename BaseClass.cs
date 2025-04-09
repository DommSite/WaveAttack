using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseClass{
        public Vector2   position{get; set;}
        protected Texture2D texture;

        protected float scale;
        
        public bool isActive{get; set;} = true;
        public Rectangle hitBox; //=> new((int)position.X, (int)position.Y, texture.Width, texture.Height);       

        public BaseClass(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            hitBox = new Rectangle((int)this.position.X, (int)this.position.Y, this.texture.Width, this.texture.Height);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //if (isActive)
            //{
                spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            //}
        }
    }       
}