using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseClass{
        public Vector2   position{get; set;}
        public Texture2D texture;

        protected float scale;
        
        public bool isActive{get; set;} = true;
        protected Vector2 rectangleSize;
        public Rectangle hitBox; 
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected float rotation = 0;

        public BaseClass(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            rectangleSize = new Vector2(texture.Width / 2, texture.Height / 2);
            hitBox = new Rectangle((int)this.position.X, (int)this.position.Y, this.texture.Width, this.texture.Height);
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isActive)
            {
                spriteBatch.Draw(texture, position, null, Color.White, 0, rectangleSize, scale, spriteEffects, 0f);
            }
        }

        
    }       
}