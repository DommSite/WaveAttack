using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace WaveAttack
{
    public class HUD
    {
        private Player player;
        private SpriteFont font;
        private List<Weapon> inventory;
        private Texture2D whitePixel;
        int weaponSlotSize;

        public HUD(Player player, GraphicsDevice graphicsDevice){
            this.player = player;
            whitePixel = new Texture2D(graphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch spriteBatch){
            Vector2 startPosition = new Vector2(20,20);
            Vector2 weaponPosition = new Vector2(startPosition.X, startPosition.Y + 60);
            inventory = player.GetWeapons();
            font = FileManager.GetFont("GameFont");
            weaponSlotSize = 32;




            spriteBatch.Draw(whitePixel, new Rectangle((int)startPosition.X, (int)startPosition.Y, player.health, 20), Color.Red);

            spriteBatch.Draw(whitePixel, new Rectangle((int)startPosition.X, (int)startPosition.Y + 30, player.stamina, 15), Color.Blue);

            for (int i = 0; i <inventory.Count; i++){
                spriteBatch.Draw(FileManager.GetTexture("WeaponSlot"), new Rectangle((int)weaponPosition.X + (i * 40), (int)weaponPosition.Y, weaponSlotSize, weaponSlotSize), Color.White);

                // Draw the weapon's icon
                spriteBatch.Draw(inventory[i].texture, new Rectangle((int)weaponPosition.X + (i * 40), (int)weaponPosition.Y, weaponSlotSize, weaponSlotSize), Color.White);

                // Draw selector around active weapon
                if (i == player.selectedWeaponSlot){
                    spriteBatch.Draw(FileManager.GetTexture("WeaponSelector"), new Rectangle((int)weaponPosition.X + (i * 40) - 2, (int)weaponPosition.Y - 2, weaponSlotSize+4, weaponSlotSize+4), Color.White);
                    spriteBatch.Draw(inventory[i].texture, new Rectangle((int)weaponPosition.X + (i * 40), (int)weaponPosition.Y, weaponSlotSize+4, weaponSlotSize+4), Color.White);
                }
            }

            spriteBatch.DrawString(font, $"HP: {player.health}/100", new Vector2(startPosition.X + 120, startPosition.Y), Color.White);
            spriteBatch.DrawString(font, $"STM: {player.stamina}/100", new Vector2(startPosition.X + 120, startPosition.Y + 30), Color.White);
        }
    }
}