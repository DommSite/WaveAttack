using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace WaveAttack
{
    public static class FileManager
    {
        private static Dictionary<string, Texture2D> textures = new();
        private static Dictionary<string, SpriteFont> fonts = new();
        public static void LoadContent(ContentManager content)
            {
                textures["Player"] = content.Load<Texture2D>("Player");
                textures["EnemyStandard"] = content.Load<Texture2D>("EnemyStandard");
                textures["EnemySniper"] = content.Load<Texture2D>("EnemySniper");
                textures["ProjectileBullet"] = content.Load<Texture2D>("ProjectileBullet");
                textures["Sword"] = content.Load<Texture2D>("Sword");
                textures["Flintlock"] = content.Load<Texture2D>("Flintlock");
                //textures["HealthBar"] = content.Load<Texture2D>("HealthBar");
                //textures["StaminaBar"] = content.Load<Texture2D>("StaminaBar");
                textures["WeaponSlot"] = content.Load<Texture2D>("WeaponSlot");
                textures["WeaponSelector"] = content.Load<Texture2D>("WeaponSelector");
                fonts["HUDFont"] = content.Load<SpriteFont>("hud_font");
            }
        public static Texture2D GetTexture(string key) => textures[key];
        public static SpriteFont GetFont(string key) => fonts[key];
    }
}
