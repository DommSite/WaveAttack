using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class BigSword : Weapon
    {
        float swingArc = MathHelper.PiOver2;
        float currentRotationOffset;
        Vector2 origin;
        Vector2 swingDir;
        bool isFlipped;
        

        
        public BigSword(BaseEntity owner) : base("BigSword", 20, 1.2f, FileManager.GetTexture("BigSword"),1, 0.025f, 1f, owner){;

        }

        public override void BeginAttack()
        {
            currentRotationOffset = -swingArc / 2f;
            hitEnemies.Clear();
            attackTimer = 0f;
            isFlipped = direction.X < 0;
            currentRotationOffset = (isFlipped ? 1 : -1) * swingArc / 2f;
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
            float swingAngle = swingArc * (isFlipped ? (1 - t) : t);
            float currentAngle = rotation + currentRotationOffset + swingAngle;

            swingDir = new Vector2((float)Math.Cos(currentAngle), (float)Math.Sin(currentAngle));
            Vector2 playerPos = GameManager.Instance.entities[0].position;
            origin =  playerPos + swingDir * (swordHeight / 2);

           /* if (isFlipped)
            {
                origin.X = 2 * playerPos.X - origin.X;
                //origin.X = playerPos.X - (origin.X - playerPos.X);
            }*/

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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!isAttacking)
                return;

            float t = attackTimer / attackDuration;
            float swingAngle = swingArc * (isFlipped ? (1 - t) : t);
            float currentAngle = rotation + currentRotationOffset + swingAngle + MathF.PI * 2;
            //float drawRotation = isFlipped ? currentAngle - MathF.PI : currentAngle;

            spriteBatch.Draw(
                texture,
                origin,
                null,
                Color.White,
                currentAngle,
                new Vector2(texture.Width / 2f, texture.Height / 2f), // origin of texture
                scale,
                isFlipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f
            );
        }







    }
}