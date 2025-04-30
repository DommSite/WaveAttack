using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace WaveAttack
{
    public class MenuManager
    {
        GameState currentState;
        private List<Button> mainMenuButtons;
        private List<Button> pauseMenuButtons;
        private List<Button> confirmMenuButtons;
        private Button backButton;
        private Button ContinueButton;
        private List<ISettingsElement> settingsMenuElements;
        private SpriteFont font = FileManager.GetFont("GameFont");
        private Texture2D overlayTexture;
        private float overlayAlpha = 0f;
        private float overlayFadeSpeed = 3f;
        private List<LeaderboardEntry> leaderboardEntries;
        private Vector2 leaderboardSize = new Vector2(500, 600);
        public static Texture2D pixel;
        private int score;
        private string playerName = "";
        private const int MaxNameLength = 3;
        private KeyboardState previousKeyboardState;


    

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

            settingsMenuElements = new List<ISettingsElement>()
            {
                new Slider(new Rectangle(300, 200, 200, 10),
                    () => GameManager.Instance.settings.Volume,
                    val => { GameManager.Instance.settings.Volume = val; GameManager.Instance.settings.Save(); }),

                new Toggle(new Rectangle(300, 250, 20, 20),
                    "Fullscreen",
                    () => GameManager.Instance.settings.Fullscreen,
                    val => { GameManager.Instance.settings.Fullscreen = val; GameManager.Instance.settings.Save(); })
            };

            confirmMenuButtons = new List<Button>()
            {
                new Button(new Rectangle(300, 260, 200, 50), "Yes", () => {GameManager.Instance.ResetGame(); GameManager.Instance.ChangeState(GameState.MainMenu);}),
                new Button(new Rectangle(300, 320, 200, 50), "No", () => {GameManager.Instance.ChangeState(GameState.Paused);}),
                
                
            };
            settingsMenuElements.Add(new ButtonAdapter(backButton));

            leaderboardEntries = new List<LeaderboardEntry>()
            {
                new LeaderboardEntry("AAA", 15000),
                new LeaderboardEntry("BBB", 12200),
                new LeaderboardEntry("CCC", 9000),
                new LeaderboardEntry("DDD", 8700),
                new LeaderboardEntry("EEE", 5000),
                new LeaderboardEntry("AAB", 15000),
                
            };
            foreach(var entry in leaderboardEntries){
                LeaderboardEntry.AddEntry(entry);
            }
            

            backButton = new Button(new Rectangle(10, 10, 100, 40), "Back", () => GameManager.Instance.ReturnToPreviousState());

            ContinueButton = new Button(new Rectangle((Game1.Instance.GraphicsDevice.Viewport.Width - 250) / 2,/* center horizontally*/   Game1.Instance.GraphicsDevice.Viewport.Height - 60,/* push closer to bottom*/   250,60),"Press here to Continue",() =>
            {
                if (LeaderboardEntry.IsViableEntry(score))
                {
                    GameManager.Instance.ChangeState(GameState.EnterName);
                }
                else
                {
                    GameManager.Instance.ChangeState(GameState.MainMenu);
                }
            });
        }

        public void Update(GameState currentState)
        {
            MouseState mState = Mouse.GetState();
            KeyboardState kState = Keyboard.GetState();

            overlayAlpha = MathHelper.Clamp(overlayAlpha + ((currentState == GameState.ConfirmBackToMenu ? 1 : -1) * overlayFadeSpeed * (1f / 60f)),0f, 0.6f);


            if (currentState == GameState.MainMenu)
                mainMenuButtons.ForEach(b => b.Update(mState));
            else if (currentState == GameState.Paused)
                pauseMenuButtons.ForEach(b => b.Update(mState));
            else if (currentState == GameState.ConfirmBackToMenu)
                confirmMenuButtons.ForEach(b => b.Update(mState));
            else if (currentState == GameState.Leaderboard)
                backButton.Update(mState);
            else if (currentState == GameState.EnterName){
                
            }
                
            else if (currentState == GameState.GameOver)
                ContinueButton.Update(mState);
            else if (currentState == GameState.Settings)
                settingsMenuElements.ForEach(e => e.Update(mState));
                
            
        }

        public void Draw(SpriteBatch spriteBatch, GameState currentState)
        {
            this.currentState = currentState;
            if (this.currentState == GameState.MainMenu)
                mainMenuButtons.ForEach(b => b.Draw(spriteBatch));

            else if (this.currentState == GameState.Paused)
                pauseMenuButtons.ForEach(b => b.Draw(spriteBatch));

            else if (this.currentState == GameState.ConfirmBackToMenu)
            {
                var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
                spriteBatch.Draw(overlayTexture, screen, Color.Black * overlayAlpha);
                UIHelper.DrawCenteredText(spriteBatch, font, "Are you sure? Progress will be lost!", new Rectangle(0, 150, screen.Width, 50), Color.White);
                confirmMenuButtons.ForEach(b => b.Draw(spriteBatch));
            }
            else if (this.currentState == GameState.Leaderboard)
            {
                backButton.Draw(spriteBatch);

                UIHelper.DrawSplitLeaderboard(
                    spriteBatch: spriteBatch,
                    font: font,
                    entries: LeaderboardEntry.LoadList(),
                    graphicsDevice: Game1.Instance.GraphicsDevice,
                    pixel: pixel
                );
            }
            else if (currentState == GameState.EnterName){
                UpdateNameEntry(spriteBatch);
                /*var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
                spriteBatch.Draw(overlayTexture, screen, Color.Black * 0.8f);

                UIHelper.DrawCenteredText(spriteBatch, font, "New High Score!", new Rectangle(0, 150, screen.Width, 50), Color.Yellow);
                UIHelper.DrawCenteredText(spriteBatch, font, "Enter Your Initials:", new Rectangle(0, 250, screen.Width, 50), Color.White);
                UIHelper.DrawCenteredText(spriteBatch, font, playerName + "_", new Rectangle(0, 310, screen.Width, 50), Color.Cyan);*/
            }
            else if (currentState == GameState.GameOver)
            {
                var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
                spriteBatch.Draw(overlayTexture, screen, Color.Black * 0.8f);
                UIHelper.DrawCenteredText(spriteBatch, font, "Game Over", new Rectangle(0, 200, screen.Width, 50), Color.White);
                ContinueButton.Draw(spriteBatch);
            }
            else if (currentState == GameState.Settings)
                settingsMenuElements.ForEach(e => e.Draw(spriteBatch));
           

        }

        public void OnPlayerDeath(int score)
        {
            this.score = score;
            
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
        private void UpdateNameEntry(SpriteBatch spriteBatch)
        {
            var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
            spriteBatch.Draw(overlayTexture, screen, Color.Black * 0.8f);

            UIHelper.DrawCenteredText(spriteBatch, font, "New High Score!", new Rectangle(0, 150, screen.Width, 50), Color.Yellow);
            UIHelper.DrawCenteredText(spriteBatch, font, "Enter Your Initials:", new Rectangle(0, 250, screen.Width, 50), Color.White);
            string nameDisplay = playerName;
            if (playerName.Length < 3)
            {
                nameDisplay += new string('_', 3 - playerName.Length);
            }
            UIHelper.DrawCenteredText(spriteBatch, font, nameDisplay, new Rectangle(0, 310, screen.Width, 100), Color.Cyan);
            
            /*int underscoreCount = 3 - playerName.Length;
            string underscoreLines = new string('\n', underscoreCount - 1) + "_";
            UIHelper.DrawCenteredText(spriteBatch, font, playerName + underscoreLines, new Rectangle(0, 310, screen.Width, 100), Color.Cyan);*/

            KeyboardState ks = Keyboard.GetState();
            

            foreach (var key in ks.GetPressedKeys())
            {
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    if (key >= Keys.A && key <= Keys.Z && playerName.Length < 3)
                    {
                        playerName += key.ToString().Substring(0, 1); // Avoid "Keys.A" becoming "A"
                    }
                    else if (key == Keys.Back && playerName.Length > 0)
                    {
                        playerName = playerName.Substring(0, playerName.Length - 1);
                    }
                    else if (key == Keys.Enter && playerName.Length == 3)
                    {
                        LeaderboardEntry.AddEntry(new LeaderboardEntry(playerName, score));
                        playerName = "";
                        GameManager.Instance.previousState = GameState.MainMenu;
                        GameManager.Instance.ChangeState(GameState.Leaderboard);
                    }
                }
            }
            previousKeyboardState = ks;
        }

        private void HandleNameInput(KeyboardState currentKeyboardState)
        {
            // Only allow name input when not already filled up
            if (playerName.Length < MaxNameLength)
            {
                foreach (var key in currentKeyboardState.GetPressedKeys())
                {
                    // Check if it's a valid letter or backspace (to delete)
                    if (key >= Keys.A && key <= Keys.Z && !previousKeyboardState.IsKeyDown(key))
                    {
                        playerName += key.ToString().Substring(0, 1); // Only take the first letter of the key
                    }
                    else if (key == Keys.Back && playerName.Length > 0 && !previousKeyboardState.IsKeyDown(key))
                    {
                        playerName = playerName.Substring(0, playerName.Length - 1); // Remove last character on Backspace
                    }
                }
            }

            // Submit name when player presses Enter (if the name is valid)
            if (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter) && playerName.Length > 0)
            {
                LeaderboardEntry.AddEntry(new LeaderboardEntry(playerName, score));
                
                GameManager.Instance.ChangeState(GameState.Leaderboard); // Go to leaderboard after submitting name
            }
        }
    }
}