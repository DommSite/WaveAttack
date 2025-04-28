using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace WaveAttack
{
    public class MenuManager
    {
        private List<Button> mainMenuButtons;
        private List<Button> pauseMenuButtons;
        private List<Button> confirmMenuButtons;
        private Button backButton;
        private SpriteFont font = FileManager.GetFont("GameFont");
        private Texture2D overlayTexture;
        private float overlayAlpha = 0f;
        private float overlayFadeSpeed = 3f;
        private List<LeaderboardEntry> leaderboardEntries;
        private Vector2 leaderboardSize = new Vector2(500, 600);
        private static Texture2D pixel;


    

        public MenuManager(GraphicsDevice graphicsDevice)
        {
            overlayTexture = new Texture2D(graphicsDevice, 1, 1);
            overlayTexture.SetData(new[] { new Color(0, 0, 0, 150) });

            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            Button.LoadContent(graphicsDevice); 

            InitializeMenus();
        }

        private void InitializeMenus()
        {
            mainMenuButtons = new List<Button>()
            {
                new Button(new Rectangle(300, 200, 200, 50), "Start", () => GameManager.Instance.ChangeState(GameState.Playing)),
                new Button(new Rectangle(300, 260, 200, 50), "Leaderboard", () => GameManager.Instance.ChangeState(GameState.Leaderboard)),
                new Button(new Rectangle(300, 320, 200, 50), "Settings", () => GameManager.Instance.ChangeState(GameState.Settings)),
                new Button(new Rectangle(300, 380, 200, 50), "Exit", () => GameManager.Instance.Exit())
            };

            pauseMenuButtons = new List<Button>()
            {
                new Button(new Rectangle(300, 200, 200, 50), "Continue", () => GameManager.Instance.ChangeState(GameState.Playing)),
                new Button(new Rectangle(300, 260, 200, 50), "Leaderboard", () => GameManager.Instance.ChangeState(GameState.Leaderboard)),
                new Button(new Rectangle(300, 320, 200, 50), "Settings", () => GameManager.Instance.ChangeState(GameState.Settings)),
                new Button(new Rectangle(300, 380, 200, 50), "MainMenu", () => GameManager.Instance.ChangeState(GameState.ConfirmBackToMenu))
            };

            confirmMenuButtons = new List<Button>()
            {
                new Button(new Rectangle(300, 260, 200, 50), "Yes", () => {GameManager.Instance.ResetGame(); GameManager.Instance.ChangeState(GameState.MainMenu);}),
                new Button(new Rectangle(300, 320, 200, 50), "No", () => {GameManager.Instance.ChangeState(GameState.Paused);}),
            };

            leaderboardEntries = new List<LeaderboardEntry>()
            {
                new LeaderboardEntry("AAA", 15000),
                new LeaderboardEntry("BBB", 12200),
                new LeaderboardEntry("CCC", 9000),
                new LeaderboardEntry("DDD", 8700),
                new LeaderboardEntry("EEE", 5000),
            };

            backButton = new Button(new Rectangle(10, 10, 100, 40), "Back", () => GameManager.Instance.ChangeState(GameState.MainMenu));
        }

        public void Update(GameState currentState)
        {
            MouseState mouse = Mouse.GetState();
            if (currentState == GameState.ConfirmBackToMenu)
            {
                
                overlayAlpha += overlayFadeSpeed * (float)(1f / 60f); 
                overlayAlpha = MathHelper.Clamp(overlayAlpha, 0f, 0.6f); 
            }
            else
            {
                
                overlayAlpha -= overlayFadeSpeed * (float)(1f / 60f);
                overlayAlpha = MathHelper.Clamp(overlayAlpha, 0f, 0.6f);
            }

            if (currentState == GameState.MainMenu)
            {
                foreach (var button in mainMenuButtons)
                    button.Update(mouse);
            }
            else if (currentState == GameState.Paused)
            {
                foreach (var button in pauseMenuButtons)
                    button.Update(mouse);
            }
            else if (currentState == GameState.ConfirmBackToMenu)
            {
                foreach (var button in confirmMenuButtons)
                    button.Update(mouse);
            }
            else if (currentState == GameState.Leaderboard)
            {
                backButton.Update(mouse);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameState currentState)
        {

            if (currentState == GameState.MainMenu)
            {
                foreach (var button in mainMenuButtons)
                    button.Draw(spriteBatch);
            }

            else if (currentState == GameState.Paused)
            {
                foreach (var button in pauseMenuButtons)
                    button.Draw(spriteBatch);
            }

             else if (currentState == GameState.ConfirmBackToMenu)
            {
                spriteBatch.Draw(overlayTexture, new Rectangle(0, 0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height),Color.Black * overlayAlpha);

                spriteBatch.DrawString(font, "Are you sure? Progress will be lost!", new Vector2(220, 200), Color.White);

                foreach(var button in confirmMenuButtons)
                    button.Draw(spriteBatch);
            }

            else if (currentState == GameState.Leaderboard)
            {
                // Draw back button
                backButton.Draw(spriteBatch);

                // Calculate center position
                int screenWidth = Game1.Instance.GraphicsDevice.Viewport.Width;
                int screenHeight = Game1.Instance.GraphicsDevice.Viewport.Height;
                Vector2 leaderboardSize = new Vector2(screenWidth * 0.6f, screenHeight * 0.7f); // Resize based on window
                Vector2 leaderboardPosition = new Vector2((screenWidth - leaderboardSize.X) / 2, (screenHeight - leaderboardSize.Y) / 2);

                // Draw leaderboard background
                spriteBatch.Draw(pixel, new Rectangle(
                    (int)leaderboardPosition.X, (int)leaderboardPosition.Y,
                    (int)leaderboardSize.X, (int)leaderboardSize.Y), Color.DarkSlateGray);

                // Draw leaderboard title
                Vector2 titlePosition = new Vector2(leaderboardPosition.X + leaderboardSize.X / 2, leaderboardPosition.Y + 20);
                var titleText = "LEADERBOARD";
                Vector2 titleOrigin = font.MeasureString(titleText) / 2;
                spriteBatch.DrawString(font, titleText, titlePosition, Color.White, 0f, titleOrigin, 1.2f, SpriteEffects.None, 0f);

                // Draw column headers (optional)
                Vector2 nameHeaderPosition = new Vector2(leaderboardPosition.X + 50, leaderboardPosition.Y + 80);
                Vector2 scoreHeaderPosition = new Vector2(leaderboardPosition.X + leaderboardSize.X - 150, leaderboardPosition.Y + 80);
                spriteBatch.DrawString(font, "NAME", nameHeaderPosition, Color.LightGray);
                spriteBatch.DrawString(font, "SCORE", scoreHeaderPosition, Color.LightGray);

                // Draw leaderboard entries
                int startY = 120;
                int lineHeight = 40;
                for (int i = 0; i < leaderboardEntries.Count; i++)
                {
                    var entry = leaderboardEntries[i];

                    Rectangle entryBox = new Rectangle((int)(leaderboardPosition.X + 30),(int)(leaderboardPosition.Y + startY + i * lineHeight),(int)(leaderboardSize.X - 60),lineHeight - 10);
                    spriteBatch.Draw(pixel, entryBox, Color.Black * 0.5f);

                    Vector2 namePosition = new Vector2(leaderboardPosition.X + 50, leaderboardPosition.Y + startY + i * lineHeight);
                    Vector2 scorePosition = new Vector2(leaderboardPosition.X + leaderboardSize.X - 150, leaderboardPosition.Y + startY + i * lineHeight);

                    spriteBatch.DrawString(font, entry.Name, namePosition, Color.White);
                    spriteBatch.DrawString(font, entry.Score.ToString(), scorePosition, Color.White);
                }
            }

        }
    }
}