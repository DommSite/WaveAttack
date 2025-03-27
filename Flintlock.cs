using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class Flintlock : Weapon
    {
        private Texture2D bulletTexture;
        public Flintlock(Texture2D texture, Texture2D bulletTexture) : base("Flintlock", 40, 2f, texture){
            this.bulletTexture = bulletTexture;
        }

        public override void Use(Player player){
            
        }
    }
}