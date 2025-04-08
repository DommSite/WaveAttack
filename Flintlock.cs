using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class Flintlock : Weapon
    {
        public Flintlock() : base("Flintlock", 40, 2f, FileManager.GetTexture("Flintlock"),2){
        }

        public override void Use(Player player){
            
        }
    }
}