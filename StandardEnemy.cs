using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class StandardEnemy : BaseEnemy
    {

        public StandardEnemy(Vector2 position)
            : base(FileManager.GetTexture("EnemyStandard"), position, 0.05f, 50, 2f, 1, 15)
        {

        }

        public override void Attack(GameTime gameTime){
                   
        }

       


        /*public override void Die(){
        }*/
    }
}