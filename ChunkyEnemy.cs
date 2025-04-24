using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class ChunkyEnemy : BaseEnemy
    {
        public ChunkyEnemy(Vector2 position) : base(FileManager.GetTexture("ChunkyEnemy"), position, 0.05f, 150, 1.5f, 1.5, 15){
            weapon = new BigSword(this);
        }
    }
}