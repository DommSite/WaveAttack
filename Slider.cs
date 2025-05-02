using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Slider : ISettingsElement
    {
        private Rectangle bar;
        private Rectangle knob;
        private Func<float> getter;
        private Action<float> setter;
        private float min, max;
        private string label;

        public Slider(Rectangle bar, Func<float> getter, Action<float> setter, string label = "", float min = 0, float max = 1)
        {
            this.bar = bar;
            this.getter = getter;
            this.setter = setter;
            this.label = label;
            this.min = min;
            this.max = max;
            UpdateKnob();
        }

        

        public void SetPosition(Vector2 position)
        {
            bar.X = (int)position.X;
            bar.Y = (int)position.Y;
            knob.Y = bar.Y - 5;
            UpdateKnob();
        }

        private void UpdateKnob()
        {
            float value = getter();
            knob.X = (int)(bar.X + (value - min) / (max - min) * bar.Width) - 5;
            knob.Width = 10;
            knob.Height = bar.Height + 10;
        }

        public void Update(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && bar.Contains(mouse.Position))
            {
                float value = (float)(mouse.X - bar.X) / bar.Width * (max - min) + min;
                setter(MathHelper.Clamp(value, min, max));
                UpdateKnob();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(label))
            {
                Vector2 labelSize = FileManager.GetFont("GameFont").MeasureString(label);
                Vector2 labelPosition = new Vector2(
                    bar.X + (bar.Width - labelSize.X) / 2,
                    bar.Y - 25
                );
                spriteBatch.DrawString(FileManager.GetFont("GameFont"), label, labelPosition, Color.White);
            }
            spriteBatch.Draw(MenuManager.pixel, bar, Color.Gray);
            spriteBatch.Draw(MenuManager.pixel, knob, Color.White);
        }
    }
}