using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WaveAttack.Entities;
using WaveAttack.Entities.Enemies;

namespace WaveAttack.Weapons
{
    public class BigSword : Weapon
    {
        private float swingArc = MathHelper.PiOver4; // 45 degrees
        private float startRotation;
        private float endRotation;
        private float currentRotation;
        private float distanceFromOwner = 20f; // <-- customizable distance
        private bool isFlipped;
        protected Vector2[] hitBoxVertices;
        protected float angle;

        
        public BigSword(BaseEntity owner) : base("BigSword", 200, 1.2f, FileManager.GetTexture("BigSword"),1, 0.025f, 1f, owner, FileManager.GetSound("Slash"), 1, 1, 1){

        }

        public override void BeginAttack(){
            hitTargets.Clear();
            attackTimer = 0f;
            isFlipped = direction.X < 0;

            rotation = (float)Math.Atan2(direction.Y, direction.X) + (float)Math.PI  / 2f;
            startRotation = rotation - swingArc;
            endRotation = rotation + swingArc;
            /*if(!isFlipped){
                spriteEffects = SpriteEffects.FlipVertically;
            }*/
        }

        public override void ContinueAttack(GameTime gameTime){
            float progress = attackTimer / attackDuration;
            currentRotation = isFlipped ? MathHelper.Lerp(endRotation, startRotation, progress) : MathHelper.Lerp(startRotation, endRotation, progress);
        
            position = owner.position + (new Vector2(MathF.Cos(currentRotation - MathHelper.PiOver2), MathF.Sin(currentRotation - MathHelper.PiOver2)) * distanceFromOwner);

            Vector2 forward = new Vector2(MathF.Cos(currentRotation - MathHelper.PiOver2), MathF.Sin(currentRotation - MathHelper.PiOver2)) * distanceFromOwner;
            if (forward != Vector2.Zero){
                forward.Normalize();
            }
                
            Vector2 perp = new Vector2(-forward.Y, forward.X);

            Vector2 swordForward = forward * (swordHeight / 2f);
            Vector2 swordSide = perp * (swordWidth / 2f);

            Vector2 topLeft = position - swordForward - swordSide;
            Vector2 topRight = position - swordForward + swordSide;
            Vector2 bottomLeft = position + swordForward - swordSide;
            Vector2 bottomRight = position + swordForward + swordSide;

            hitBoxVertices = new[] { topLeft, topRight, bottomRight, bottomLeft };

            //UpdateRotatedHitBox();

            //var swordVertices = GetSwordVertices();

            foreach (var entity in GameManager.Instance.entities)
            {
                if(entity is BaseEntity target){
                    if((owner is Player && entity is BaseEnemy)||(owner is BaseEnemy && entity is Player)){
                        if (GameManager.Instance.IsPointInsidePolygon(entity.hitBox, hitBoxVertices))
                        {
                            target.TakeDamage(damage);
                            hitTargets.Add(target);
                        }
                    }
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            float rotation = currentRotation - MathHelper.PiOver2 + MathHelper.PiOver4;
            if (!isAttacking)
                return;
            System.Console.WriteLine(currentRotation);
            

            spriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                rotation,
                rectangleSize,
                scale,
                spriteEffects,
                0f
            );
        }
    }
}