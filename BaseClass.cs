using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class BaseClass{
        public Vector2   position{get; set;}
        protected Texture2D texture;
        
        public bool isActive{get; set;} = true;
        

        public BaseClass(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public virtual void Update(GameTime gameTime){
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }       
}