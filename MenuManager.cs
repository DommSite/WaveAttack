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

        private Game1 game;

        public MenuManager(Game1 game)
        {
            this.game = game;

            Button.LoadContent(); 

            InitializeMenus();
        }

        private void InitializeMenus()
        {
            mainMenuButtons = new List<Button>()
            {
                new Button(new Rectangle(300, 200, 200, 50), "Start", () => game.ChangeState(GameState.Playing)),
                new Button(new Rectangle(300, 260, 200, 50), "Leaderboard", () => game.ChangeState(GameState.Leaderboard)),
                new Button(new Rectangle(300, 320, 200, 50), "Settings", () => game.ChangeState(GameState.Settings)),
                new Button(new Rectangle(300, 380, 200, 50), "Exit", () => game.Exit())
            };

            pauseMenuButtons = new List<Button>()
            {
                new Button(new Rectangle(300, 200, 200, 50), "Continue", () => game.ChangeState(GameState.Playing)),
                new Button(new Rectangle(300, 260, 200, 50), "Leaderboard", () => game.ChangeState(GameState.Leaderboard)),
                new Button(new Rectangle(300, 320, 200, 50), "Settings", () => game.ChangeState(GameState.Settings)),
                new Button(new Rectangle(300, 380, 200, 50), "Exit", () => game.ChangeState(GameState.MainMenu))
            };
        }

        public void Update(GameState currentState)
        {
            MouseState mouse = Mouse.GetState();

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
        }

        public void Draw(SpriteBatch spriteBatch, GameState currentState)
        {
            spriteBatch.Begin();

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

            spriteBatch.End();
        }
    }
}