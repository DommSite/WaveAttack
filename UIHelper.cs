using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace WaveAttack
{
    public static class UIHelper
    {
        /// <summary>
        /// Draws text centered within the given rectangle.
        /// </summary>
        public static void DrawCenteredText(SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle bounds, Color color)
        {
            Vector2 textSize = font.MeasureString(text);
            Vector2 position = new Vector2(
                bounds.X + (bounds.Width - textSize.X) / 2,
                bounds.Y + (bounds.Height - textSize.Y) / 2
            );
            spriteBatch.DrawString(font, text, position, color);
        }

        /// <summary>
        /// Draws a vertical leaderboard or list, centered or custom-positioned on screen.
        /// </summary>
        public static void DrawVerticalList(
            SpriteBatch spriteBatch,
            SpriteFont font,
            List<string> items,
            GraphicsDevice graphicsDevice,
            Texture2D pixel,
            Color textColor,
            Color boxColor,
            bool centerOnScreen = true,
            int maxItems = 10,
            int spacing = 10)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            // Dynamically calculate size relative to screen
            int listWidth = screenWidth / 3;
            int listHeight = screenHeight / 2;
            int startX = centerOnScreen ? (screenWidth - listWidth) / 2 : 100;
            int startY = centerOnScreen ? (screenHeight - listHeight) / 2 : 100;

            int itemHeight = (listHeight - spacing * (maxItems - 1)) / maxItems;

            for (int i = 0; i < maxItems; i++)
            {
                string text = i < items.Count ? items[i] : "---";
                Rectangle itemRect = new Rectangle(startX, startY + i * (itemHeight + spacing), listWidth, itemHeight);

                // Podium colors for top 3
                Color slotColor = i switch
                {
                    0 => Color.Gold,
                    1 => Color.Silver,
                    2 => Color.SandyBrown,
                    _ => boxColor
                };

                spriteBatch.Draw(pixel, itemRect, slotColor);
                DrawCenteredText(spriteBatch, font, text, itemRect, textColor);
            }
        }

        public static void DrawSplitLeaderboard(
    SpriteBatch spriteBatch,
    SpriteFont font,
    List<LeaderboardEntry> entries,
    GraphicsDevice graphicsDevice,
    Texture2D pixel,
    int maxEntries = 10,
    float widthRatio = 0.5f,
    float heightRatio = 0.6f,
    int margin = 40,
    int padding = 10,
    Color backgroundColor = default,
    Color boxColor = default,
    Color textColor = default)
{
    if (backgroundColor == default) backgroundColor = Color.DarkSlateGray;
    if (boxColor == default) boxColor = Color.Black * 0.5f;
    if (textColor == default) textColor = Color.White;

    var screen = graphicsDevice.Viewport.Bounds;
    var totalWidth = screen.Width * widthRatio;
    var totalHeight = screen.Height * heightRatio;
    var position = new Vector2((screen.Width - totalWidth) / 2, (screen.Height - totalHeight) / 2);

    // Background board
    spriteBatch.Draw(pixel, new Rectangle((int)position.X, (int)position.Y, (int)totalWidth, (int)totalHeight), backgroundColor);

    // Title
    var title = "LEADERBOARD";
    var titleSize = font.MeasureString(title);
    spriteBatch.DrawString(font, title,
        new Vector2(position.X + totalWidth / 2, position.Y + 20),
        textColor, 0f, titleSize / 2, 1.2f, SpriteEffects.None, 0f);

    // Header row
    int rowHeight = ((int)(totalHeight - 80) - ((maxEntries + 1) * margin)) / maxEntries;
    var startY = position.Y + 60 + margin;

    for (int i = 0; i < maxEntries; i++)
    {
        var entry = i < entries.Count ? entries[i] : new LeaderboardEntry("---", 0);

        Rectangle rowRect = new Rectangle(
            (int)position.X + margin,
            (int)(startY + i * (rowHeight + margin)),
            (int)totalWidth - 2 * margin,
            rowHeight
        );

        // Row box
        spriteBatch.Draw(pixel, rowRect, boxColor);

        // Name and score text
        string name = entry.Name;
        string score = i < entries.Count ? entry.Score.ToString() : "---";

        Vector2 nameSize = font.MeasureString(name);
        Vector2 scoreSize = font.MeasureString(score);

        float spacing = 50f; // space between name and score
        float combinedWidth = nameSize.X + spacing + scoreSize.X;

        float startX = rowRect.X + (rowRect.Width - combinedWidth) / 2;

        Vector2 namePos = new Vector2(startX, rowRect.Y + (rowRect.Height - nameSize.Y) / 2);
        Vector2 scorePos = new Vector2(startX + nameSize.X + spacing, rowRect.Y + (rowRect.Height - scoreSize.Y) / 2);

        spriteBatch.DrawString(font, name, namePos, textColor);
        spriteBatch.DrawString(font, score, scorePos, textColor);
    }
}
    }
}