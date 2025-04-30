using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Toggle : ISettingsElement
    {
        Rectangle rect;
        string label;
        Func<bool> getter;
        Action<bool> setter;

        public Toggle(Rectangle rect, string label, Func<bool> getter, Action<bool> setter)
        {
            this.rect = rect;
            this.label = label;
            this.getter = getter;
            this.setter = setter;
        }

        public void Update(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && rect.Contains(mouse.Position))
            {
                setter(!getter());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MenuManager.pixel, rect, getter() ? Color.Green : Color.Red);
            spriteBatch.DrawString(FileManager.GetFont("GameFont"), label, new Vector2(rect.X + rect.Width + 10, rect.Y), Color.White);
        }
    }
}