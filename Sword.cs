using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Sword : Weapon
    {
        float swordWidth;// = 50f;
        float swordHeight;// = 150f;
        float scale = 0.02f;
        
        
        Vector2 hitBoxCenter;


        
        float slashDuration = 0.5f; 
        

        public Sword() : base("Sword", 15, 1.5f, FileManager.GetTexture("Sword"), 0, 0.5f){
            swordWidth = texture.Width * scale;
            swordHeight = texture.Height * scale;
            swordHitBoxVertices = new Vector2[4];
        }



        public override void Update(GameTime gameTime){
            if(!canAttack){
                cooldownTimer += gameTime.ElapsedGameTime;
                if(cooldownTimer.TotalSeconds >= cooldownTime){
                    canAttack = true;
                    cooldownTimer = TimeSpan.Zero;
                }
            }
            
            if (!isAttacking){
                return;
            }
        
            slashTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (slashTimer >= slashDuration)
            {
                isAttacking = false;
                return;
            }

            float progress = slashTimer / slashDuration;
            float movementFactor = (float)Math.Sin(progress * Math.PI);
            float thrustDistance = swordHeight * 0.6f;
            hitBoxCenter = GameManager.Instance.entities[0].position + direction * thrustDistance * movementFactor;

            Vector2 perp = new Vector2(-direction.Y, direction.X);
            Vector2 forward = direction * (swordHeight / 2);
            Vector2 side = perp * (swordWidth / 2);

            swordHitBoxVertices[0] = hitBoxCenter - forward - side;
            swordHitBoxVertices[1] = hitBoxCenter - forward + side;
            swordHitBoxVertices[2] = hitBoxCenter + forward + side;
            swordHitBoxVertices[3] = hitBoxCenter + forward - side;

            foreach (var entity in GameManager.Instance.entities)
            {
                if (entity is BaseEnemy enemy && enemy.isActive && !hitEnemies.Contains(enemy))
                {
                    if (GameManager.Instance.IsPointInsidePolygon(enemy.hitBox, swordHitBoxVertices))
                    {
                        enemy.TakeDamage(damage);
                        hitEnemies.Add(enemy);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!isAttacking){
                return;
            } 

            spriteBatch.Draw(texture, hitBoxCenter, null, Color.White, rotation + MathF.PI / 4f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);  
            
        }
    }
}