using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class BigSword : Weapon
    {
        public BigSword() : base("BigSword", 20, 1.2f, SpriteManager.GetTexture("BigSword")){

        }

        public override void Use(Player player){
            
        }
    }
}