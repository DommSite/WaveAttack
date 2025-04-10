using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class BigSword : Weapon
    {
        float swingArc = MathHelper.PiOver2; // 90 degrees
        
        float currentRotationOffset;
        Vector2 origin;

        

        
        public BigSword() : base("BigSword", 20, 1.2f, FileManager.GetTexture("BigSword"),1, 0.1f, 3f){;

        }

        public override void BeginAttack()
        {
            currentRotationOffset = -swingArc / 2f;
            hitEnemies.Clear();
        }

        public override void ContinueAttack(GameTime gameTime)
        {
            attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (attackTimer >= attackDuration)
            {
                isAttacking = false;
                cooldownTimer = TimeSpan.Zero;
                return;
            }
            
            
            float t = attackTimer / attackDuration;
            float swingAngle = swingArc * t;
            float currentAngle = rotation + currentRotationOffset + swingAngle;

            Vector2 swingDir = new Vector2((float)Math.Cos(currentAngle), (float)Math.Sin(currentAngle));
            origin = GameManager.Instance.entities[0].position + swingDir * (swordHeight / 2);

            Vector2 perp = new Vector2(-swingDir.Y, swingDir.X);
            Vector2 forward = swingDir * (swordHeight);
            Vector2 side = perp * (swordWidth);

            Vector2 topLeft = origin - forward - side;
            Vector2 topRight = origin - forward + side;
            Vector2 bottomLeft = origin + forward - side;
            Vector2 bottomRight = origin + forward + side;

            Vector2[] hitBox = new[] { topLeft, topRight, bottomRight, bottomLeft };

            foreach (var entity in GameManager.Instance.entities)
            {
                if (entity is BaseEnemy enemy && enemy.isActive && !hitEnemies.Contains(enemy))
                {
                    if (GameManager.Instance.IsPointInsidePolygon(enemy.hitBox, hitBox))
                    {
                        enemy.TakeDamage(damage);
                        hitEnemies.Add(enemy);
                    }
                }
            }
        }







    }
}