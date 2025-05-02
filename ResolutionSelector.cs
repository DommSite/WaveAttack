using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class ResolutionSelector : ISettingsElement
    {
        private Rectangle bounds;
        private Point[] options;
        private int selectedIndex;
        private Func<Point> getter;
        private Action<Point> setter;
        private bool wasPressedLastFrame = false;

        private static SpriteFont font => FileManager.GetFont("GameFont");

        public ResolutionSelector(Rectangle bounds, Point[] options, Func<Point> getter, Action<Point> setter)
        {
            this.bounds = bounds;
            this.options = options;
            this.getter = getter;
            this.setter = setter;
            selectedIndex = Array.FindIndex(options, o => o == getter());
            if (selectedIndex == -1) selectedIndex = 0;
        }

        public void SetPosition(Vector2 position)
        {
            bounds.Location = position.ToPoint();
        }

        public void Update(MouseState mouse)
        {
            if (bounds.Contains(mouse.Position))
            {
                if (mouse.LeftButton == ButtonState.Pressed && !wasPressedLastFrame)
                {
                    selectedIndex = (selectedIndex + 1) % options.Length;
                    setter(options[selectedIndex]);
                }
            }
            wasPressedLastFrame = mouse.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            string text = $"Resolution: {options[selectedIndex].X}x{options[selectedIndex].Y}";
            spriteBatch.Draw(MenuManager.pixel, bounds, Color.DarkSlateGray * 0.6f);
            spriteBatch.DrawString(font, text, bounds.Location.ToVector2(), Color.White);
        }
}
}