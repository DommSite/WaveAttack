using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaveAttack.Weapons;

namespace WaveAttack.Entities.Enemies{
    public class StandardEnemy : BaseEnemy
    {

        public StandardEnemy(Vector2 position)
            : base(FileManager.GetTexture("EnemyStandard"), position, 0.05f, 50, 2f, 1, 40, 10)
        {
            weapon = new Sword(this);
        }

        




        /*public override void Die(){
        }*/
    }
}