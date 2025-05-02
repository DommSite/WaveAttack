using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Toggle : ISettingsElement
    {
        private Rectangle rect;
        private string label;
        private Func<bool> getter;
        private Action<bool> setter;
        private MouseState oldMouseState;

        public Toggle(Rectangle rect, string label, Func<bool> getter, Action<bool> setter)
        {
            this.rect = rect;
            this.label = label;
            this.getter = getter;
            this.setter = setter;
        }

        public void SetPosition(Vector2 position)
        {
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
        }

        public void Update(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed &&
                oldMouseState.LeftButton == ButtonState.Released &&
                rect.Contains(mouse.Position))
            {
                setter(!getter());
            }

            oldMouseState = mouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MenuManager.pixel, rect, getter() ? Color.Green : Color.Red);
            spriteBatch.DrawString(FileManager.GetFont("GameFont"), label, new Vector2(rect.X + rect.Width + 10, rect.Y), Color.White);
        }
    }
}