using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class BigSword : Weapon
    {
        public BigSword() : base("BigSword", 20, 1.2f, FileManager.GetTexture("BigSword"),1, 1.5f){

        }

        public override void Use(GameTime gameTime, Player player){
            
        }
    }
}