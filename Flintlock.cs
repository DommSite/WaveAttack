using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class Flintlock : Weapon
    {
        public Flintlock() : base("Flintlock", 40, 2f, SpriteManager.GetTexture("Flintlock")){
        }

        public override void Use(Player player){
            
        }
    }
}