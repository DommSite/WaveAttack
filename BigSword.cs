using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class BigSword : Weapon
    {
        float swingArc = MathHelper.PiOver2; // 90 degrees
        float swingDuration = 0.4f; // seconds
        float currentSwingTime = 0f;
        

        bool isSwinging = false;
        float baseRotation;
        float currentRotation;
        
        Vector2 origin;

        float swordLength;
        float swordWidth;

        
        public BigSword() : base("BigSword", 20, 1.2f, FileManager.GetTexture("BigSword"),1, 1.5f){
            swordLength = texture.Height * scale;
            swordWidth = texture.Width * scale;
            hitEnemies = new HashSet<BaseEnemy>();
            scale = 0.1f;
        }







    }
}