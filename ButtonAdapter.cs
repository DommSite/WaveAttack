/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class ButtonAdapter : ISettingsElement
    {
        private Button button;

        public ButtonAdapter(Button button)
        {
            this.button = button;
        }

        public void SetPosition(Vector2 pos)
        {
            button.bounds = new Rectangle((int)pos.X, (int)pos.Y, button.bounds.Width, button.bounds.Height);
        }

        public void Update(MouseState m) => button.Update(m);
        public void Draw(SpriteBatch s) => button.Draw(s);
    }
}*/