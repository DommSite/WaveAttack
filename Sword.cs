using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class Sword : Weapon
    {
        public Sword() : base("Sword", 15, 1.5f, SpriteManager.GetTexture("Sword")){

        }


        public override void Use(Player player){
            
        }

    }
}