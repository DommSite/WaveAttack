using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WaveAttack.Entities;
using WaveAttack.Entities.Enemies;

namespace WaveAttack.Weapons
{
    public class Sword : Weapon
    {
        
        protected Vector2[] hitBoxVertices;

        public Sword(BaseEntity owner) : base("Sword", 15, 1.5f, FileManager.GetTexture("Sword"), 0, 0.02f, 0.5f, owner){       
            hitBoxVertices = new Vector2[4];         
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!isAttacking){
                return;
            } 
            spriteBatch.Draw(texture, position, null, Color.White, rotation, rectangleSize, scale, spriteEffects, 0f);  
            
        }

        public override void BeginAttack()
        {
            isAttacking = true;
            rotation = (float)Math.Atan2(direction.Y, direction.X) + (float)Math.PI / 4f;
            hitTargets.Clear();
        }

        public override void ContinueAttack(GameTime gameTime)
        {
            float progress = attackTimer / attackDuration;
            float movementFactor = (float)Math.Sin(progress * Math.PI);
            float thrustDistance = swordHeight * 0.6f;
            position = owner.position + direction * thrustDistance * movementFactor;


            Vector2 perp = new Vector2(-direction.Y, direction.X);
            Vector2 forward = direction * (swordHeight / 2);
            Vector2 side = perp * (swordWidth / 2);

            hitBoxVertices[0] = position - forward - side;
            hitBoxVertices[1] = position - forward + side;
            hitBoxVertices[2] = position + forward + side;
            hitBoxVertices[3] = position + forward - side;

            foreach (var entity in GameManager.Instance.entities)
            {
                if(entity is BaseEntity baseEntity){
                    if(baseEntity == owner || !baseEntity.isActive|| !((owner is Player && baseEntity is BaseEnemy) || ( owner is BaseEnemy && baseEntity is Player))){
                    continue;
                    }

                    
                    if (GameManager.Instance.IsPointInsidePolygon(baseEntity.hitBox, hitBoxVertices)){
                        if (!hitTargets.Contains(baseEntity))
                        {
                            baseEntity.TakeDamage(damage);
                            hitTargets.Add(baseEntity);
                        }
                    }
                }

                
            }
        }
    }
}