using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;



namespace WaveAttack
{
    public static class FileManager
    {
        private static Dictionary<string, Texture2D> textures = new();
        private static Dictionary<string, SpriteFont> fonts = new();
        private static Dictionary<string, SoundEffect> sounds = new();
        public static void LoadContent(ContentManager content)
            {
                textures["Player"] = content.Load<Texture2D>("Player");
                textures["EnemyStandard"] = content.Load<Texture2D>("EnemyStandard");
                textures["EnemySniper"] = content.Load<Texture2D>("EnemySniper");
                textures["ChunkyEnemy"] = content.Load<Texture2D>("ChunkyEnemy");
                textures["ProjectileBullet"] = content.Load<Texture2D>("ProjectileBullet");
                textures["Sword"] = content.Load<Texture2D>("Sword");
                textures["BigSword"] = content.Load<Texture2D>("BigSword");
                textures["Flintlock"] = content.Load<Texture2D>("Flintlock");
                textures["WeaponSlot"] = content.Load<Texture2D>("WeaponSlot");
                textures["WeaponSelector"] = content.Load<Texture2D>("WeaponSelector");
                

                
                fonts["GameFont"] = content.Load<SpriteFont>("GameFont");


                sounds["Hurt1Retro"] = content.Load<SoundEffect>("Hurt1Retro");
                sounds["Hurt2"] = content.Load<SoundEffect>("Hurt2");
                sounds["Hurt3"] = content.Load<SoundEffect>("Hurt3");
                sounds["BulletHit"] = content.Load<SoundEffect>("BulletHit");
                sounds["Gunshot"] = content.Load<SoundEffect>("Gunshot");
                sounds["Slash"] = content.Load<SoundEffect>("Slash");
                sounds["Stab"] = content.Load<SoundEffect>("Stab");
                sounds["ManGurgleDeath"] = content.Load<SoundEffect>("ManGurgleDeath");
                sounds["IrelanderProbing"] = content.Load<SoundEffect>("IrelanderProbing");
                sounds["BoneCrack"] = content.Load<SoundEffect>("BoneCrack");
                



            }
        public static Texture2D GetTexture(string key) => textures[key];
        public static SpriteFont GetFont(string key) => fonts[key];
        public static SoundEffect GetSound(string key) => sounds[key];
    }
}
