using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Slider : ISettingsElement
{
    Rectangle bar, knob;
    float min, max;
    Func<float> getter;
    Action<float> setter;

    public Slider(Rectangle bar, Func<float> getter, Action<float> setter, float min = 0, float max = 1)
    {
        this.bar = bar;
        this.getter = getter;
        this.setter = setter;
        this.min = min;
        this.max = max;
        knob = new Rectangle(0, bar.Y - 5, 10, bar.Height + 10);
        UpdateKnob();
    }

    private void UpdateKnob()
    {
        var value = getter();
        knob.X = (int)(bar.X + (value - min) / (max - min) * bar.Width) - knob.Width / 2;
    }

    public void Update(MouseState mouse)
    {
        if (mouse.LeftButton == ButtonState.Pressed && bar.Contains(mouse.Position))
        {
            float newValue = (float)(mouse.X - bar.X) / bar.Width * (max - min) + min;
            setter(MathHelper.Clamp(newValue, min, max));
            UpdateKnob();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(MenuManager.pixel, bar, Color.Gray);
        spriteBatch.Draw(MenuManager.pixel, knob, Color.White);
    }
}
}