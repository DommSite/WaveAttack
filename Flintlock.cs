using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class Flintlock : Weapon
    {
        public Flintlock() : base("Flintlock", 40, 2f, FileManager.GetTexture("Flintlock"),3){
        }

        public override void Use(Player player){
            
        }
    }
}