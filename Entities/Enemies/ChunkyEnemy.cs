using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WaveAttack.Weapons;

namespace WaveAttack.Entities.Enemies{
    public class ChunkyEnemy : BaseEnemy
    {
        public ChunkyEnemy(Vector2 position) : base(FileManager.GetTexture("ChunkyEnemy"), position, 0.05f, 150, 1.5f, 1.5, 15){
            weapon = new BigSword(this);
        }
    }
}