
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public interface ISettingsElement
    {
        void Update(MouseState mouse);
        void Draw(SpriteBatch spriteBatch);
        void SetPosition(Vector2 position);
    }
}
