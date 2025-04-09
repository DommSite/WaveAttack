using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class Sword : Weapon
    {
        public Sword() : base("Sword", 15, 1.5f, FileManager.GetTexture("Sword"), 0, 0.5f){

        }


        public override void Use(GameTime gameTime, Player player){
            if(!canAttack){
                return;
            }
            canAttack = false;

            
        }

    }
}