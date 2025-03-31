using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;



namespace WaveAttack
{
    public static class SpriteManager
    {
        private static Dictionary<string, Texture2D> textures = new();

        public static void LoadContent(ContentManager content)
            {
                textures["Player"] = content.Load<Texture2D>("Player");
                textures["EnemyStandard"] = content.Load<Texture2D>("EnemyStandard");
                textures["EnemySniper"] = content.Load<Texture2D>("EnemySniper");
                textures["ProjectileBullet"] = content.Load<Texture2D>("ProjectileBullet");
                textures["Sword"] = content.Load<Texture2D>("Sword");
                textures["Flintlock"] = content.Load<Texture2D>("Flintlock");
            }

        public static Texture2D GetTexture(string key) => textures[key];
     }
}
