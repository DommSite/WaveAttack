using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Button backButton2;
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
        private bool firstStartup = true;


    

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
            int startY = 50;         // Starting vertical position
            int spacing = 40;        // Space between each setting element
            int currentIndex = 0;
            backButton = new Button(new Rectangle(20, 20, 120, 50), "Back", () => GameManager.Instance.ReturnToPreviousState());
            backButton2 = new Button(new Rectangle(200, 200, 120, 50), "Back", () => GameManager.Instance.ReturnToPreviousState());

            mainMenuButtons = new List<Button>()
            {
                new Button(Rectangle.Empty, "Start", () => GameManager.Instance.ChangeState(GameState.Playing)),
                new Button(Rectangle.Empty, "Leaderboard", () => GameManager.Instance.ChangeState(GameState.Leaderboard)),
                new Button(Rectangle.Empty, "Settings", () => GameManager.Instance.ChangeState(GameState.Settings)),
                new Button(Rectangle.Empty, "Exit", () => GameManager.Instance.Exit())
            };
            LayoutMenuButtons(mainMenuButtons);

            pauseMenuButtons = new List<Button>()
            {
                new Button(Rectangle.Empty, "Continue", () => GameManager.Instance.ChangeState(GameState.Playing)),
                new Button(Rectangle.Empty, "Leaderboard", () => GameManager.Instance.ChangeState(GameState.Leaderboard)),
                new Button(Rectangle.Empty, "Settings", () => GameManager.Instance.ChangeState(GameState.Settings)),
                new Button(Rectangle.Empty, "MainMenu", () => GameManager.Instance.ChangeState(GameState.ConfirmBackToMenu))
            };
            LayoutMenuButtons(pauseMenuButtons);

            confirmMenuButtons = new List<Button>()
            {
                new Button(Rectangle.Empty, "Yes", () => { GameManager.Instance.ResetGame(); GameManager.Instance.ChangeState(GameState.MainMenu); }),
                new Button(Rectangle.Empty, "No", () => GameManager.Instance.ChangeState(GameState.Paused))
            };
            LayoutMenuButtons(confirmMenuButtons);

            ContinueButton = new Button(new Rectangle(0, 0, 250, 60), "Press here to Continue", () =>
            {
                if (LeaderboardEntry.IsViableEntry(score))
                    GameManager.Instance.ChangeState(GameState.EnterName);
                else
                    GameManager.Instance.ChangeState(GameState.MainMenu);
            });

            settingsMenuElements = new List<ISettingsElement>
            {
                new Slider(new Rectangle(0, startY + spacing * currentIndex++, 200, 10),
                    () => GameManager.Instance.settings.Volume,
                    val => { GameManager.Instance.settings.Volume = val; GameManager.Instance.settings.Save(); },
                    "Master Volume"),

                new Slider(new Rectangle(0, startY + spacing * currentIndex++, 200, 10),
                    () => GameManager.Instance.settings.SFXVolume,
                    val => { GameManager.Instance.settings.SFXVolume = val; GameManager.Instance.settings.Save(); },
                    "SFX Volume"),

                new Toggle(new Rectangle(0, startY + spacing * currentIndex++, 20, 20),
                    "Mute All",
                    () => GameManager.Instance.settings.MuteAll,
                    val => { GameManager.Instance.settings.MuteAll = val; GameManager.Instance.settings.Save(); }),

                new Toggle(new Rectangle(0, startY + spacing * currentIndex++, 20, 20),
                    "Fullscreen",
                    () => GameManager.Instance.settings.Fullscreen,
                    val => {
                        GameManager.Instance.settings.Fullscreen = val;
                        GameManager.Instance.settings.Apply(Game1.Instance._graphics);
                        GameManager.Instance.settings.Save();
                    }),

                new ResolutionSelector(new Rectangle(0, startY + spacing * currentIndex++, 200, 30),
                    new Point[] {
                        new Point(800, 600),
                        new Point(1280, 720),
                        new Point(1600, 900),
                        new Point(1920, 1080),
                    },
                    () => GameManager.Instance.settings.Resolution,
                    val => {
                        GameManager.Instance.settings.Resolution = val;
                        GameManager.Instance.settings.Apply(Game1.Instance._graphics);
                        GameManager.Instance.settings.Save();
                    }),
                new Slider(new Rectangle(0, startY + spacing * currentIndex++, 200, 10),
                    () => (float)GameManager.Instance.settings.totalRounds,
                    val => {
                        GameManager.Instance.settings.totalRounds = (int)val;
                        GameManager.Instance.settings.Save();
                    },
                    "Total Rounds: " + (int)GameManager.Instance.settings.totalRounds, 
                    1, 9999),
                //new ButtonAdapter(backButton2)
            };
            var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
            

            // Position ContinueButton centered bottom
            //var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
            ContinueButton.bounds = new Rectangle(
                (screen.Width - ContinueButton.bounds.Width) / 2,
                screen.Height - ContinueButton.bounds.Height - 40,
                ContinueButton.bounds.Width,
                ContinueButton.bounds.Height
            );

            if (settingsMenuElements != null)
            {
                LayoutSettingsElements(
                    settingsMenuElements,
                    startY: Game1.Instance.GraphicsDevice.Viewport.Height / 6,
                    spacing: Game1.Instance.GraphicsDevice.Viewport.Height / 12
                );
            }
        }

        public void Update(GameState currentState)
        {
            MouseState mState = Mouse.GetState();
            KeyboardState kState = Keyboard.GetState();
            MediaPlayer.Volume = GameManager.Instance.settings.Volume;
            if(GameManager.Instance.settings.MuteAll){
                MediaPlayer.Volume = 0;
            }

            overlayAlpha = MathHelper.Clamp(overlayAlpha + ((currentState == GameState.ConfirmBackToMenu ? 1 : -1) * overlayFadeSpeed * (1f / 60f)),0f, 0.6f);


            if (currentState == GameState.MainMenu){
                LayoutMenuButtons(mainMenuButtons);
                mainMenuButtons.ForEach(b => b.Update(mState));
                if(firstStartup){
                    GameManager.Instance.PlaySong(FileManager.GetSong("OpeningMusic"), false);
                    firstStartup = false;
                }
                else if(!firstStartup &&  MediaPlayer.State == MediaState.Stopped){
                    GameManager.Instance.PlaySong(FileManager.GetSong("MenuMusic"));
                }
            }
                
            else if (currentState == GameState.Paused){
                LayoutMenuButtons(pauseMenuButtons);
                pauseMenuButtons.ForEach(b => b.Update(mState));
                GameManager.Instance.PlaySong(FileManager.GetSong("MenuMusic"),true, true);
            }
                
            else if (currentState == GameState.ConfirmBackToMenu){
                LayoutMenuButtons(confirmMenuButtons);
                confirmMenuButtons.ForEach(b => b.Update(mState));
                GameManager.Instance.PlaySong(FileManager.GetSong("MenuMusic"));
            }
                
            else if (currentState == GameState.Leaderboard){
                backButton.Update(mState);
                GameManager.Instance.PlaySong(FileManager.GetSong("MenuMusic"));
            }
                
            else if (currentState == GameState.EnterName){
                
            }
                
            else if (currentState == GameState.GameOver)
                ContinueButton.Update(mState);
            else if (currentState == GameState.Settings){
                settingsMenuElements.ForEach(e => e.Update(mState));
                backButton.Update(mState);
                GameManager.Instance.PlaySong(FileManager.GetSong("MenuMusic"));
            }
                
                
            
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
                

                UIHelper.DrawSplitLeaderboard(
                    spriteBatch: spriteBatch,
                    font: font,
                    entries: LeaderboardEntry.LoadList(),
                    graphicsDevice: Game1.Instance.GraphicsDevice,
                    pixel: pixel
                );
                backButton.Draw(spriteBatch);
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
            else if (currentState == GameState.Settings){
                settingsMenuElements.ForEach(e => e.Draw(spriteBatch));
                backButton.Draw(spriteBatch);
            }
                
           
            
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

        /*private void HandleNameInput(KeyboardState currentKeyboardState)
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
        }*/
        public static void LayoutSettingsElements(List<ISettingsElement> elements, int startY, int spacing, int x)
        {
            int y = startY;
            foreach (var el in elements)
            {
                el.SetPosition(new Vector2(x, y));
                y += spacing;
            }
        }


        private void LayoutMenuButtons(List<Button> buttons, int verticalSpacing = 20)
        {
            var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
            int buttonWidth = 200;
            int buttonHeight = 50;

            int totalHeight = buttons.Count * (buttonHeight + verticalSpacing) - verticalSpacing;
            int startY = (screen.Height - totalHeight) / 2;

            for (int i = 0; i < buttons.Count; i++)
            {
                int x = (screen.Width - buttonWidth) / 2;
                int y = startY + i * (buttonHeight + verticalSpacing);
                buttons[i].bounds = new Rectangle(x, y, buttonWidth, buttonHeight);
            }
        }

        public static void LayoutSettingsElements(List<ISettingsElement> elements, int startY, int spacing)
        {
            var screen = Game1.Instance.GraphicsDevice.Viewport.Bounds;
            int x = (screen.Width - 400) / 2; // Center elements (assuming max width 400)

            int y = startY;
            foreach (var el in elements)
            {
                el.SetPosition(new Vector2(x, y));
                y += spacing;
            }
        }
    }
}