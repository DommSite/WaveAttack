using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class ButtonAdapter : ISettingsElement
    {
        private Button button;
        public ButtonAdapter(Button button) { this.button = button; }
        public void Update(MouseState m) => button.Update(m);
        public void Draw(SpriteBatch s) => button.Draw(s);
    }
}