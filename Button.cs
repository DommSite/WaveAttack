using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace WaveAttack
{
    public class Button
    {
        private Rectangle bounds;
        private string text;
        private Action onClick;
        private bool isHovered;
        private MouseState oldState;

        private static Texture2D pixel;
        private static SpriteFont font;
        

        public Button(Rectangle bounds, string text, Action onClick)
        {
            this.bounds = bounds;
            this.text = text;
            this.onClick = onClick;
        }

        public static void LoadContent(GraphicsDevice graphicsDevice)
        {
            if (pixel == null)
            {
                pixel = new Texture2D(graphicsDevice, 1, 1);
                pixel.SetData(new[] { Color.White });
            }

            if (font == null)
            {
                font = FileManager.GetFont("GameFont"); 
            }
        }

        public void Update(MouseState mouse)
        {
            isHovered = bounds.Contains(mouse.Position);

            if (isHovered && mouse.LeftButton == ButtonState.Pressed && !oldState.LeftButton.Equals(ButtonState.Pressed))
            {
                onClick?.Invoke();
            }
            oldState = mouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color buttonColor = isHovered ? Color.Yellow : Color.White;

            spriteBatch.Draw(pixel, bounds, buttonColor * 0.3f);

            Vector2 textSize = font.MeasureString(text);
            Vector2 textPos = new Vector2(
                bounds.Center.X - textSize.X / 2,
                bounds.Center.Y - textSize.Y / 2
            );

            spriteBatch.DrawString(font, text, textPos, Color.Black);
        }
    }
}