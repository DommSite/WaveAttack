using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WaveAttack.Entities;

namespace WaveAttack.Weapons
{
    public class Flintlock : Weapon
    {
        public Flintlock(BaseEntity owner) : base("Flintlock", 40, 2f, FileManager.GetTexture("Flintlock"),2, 0.04f, 1f, owner){
            spriteEffects = SpriteEffects.FlipHorizontally;
        }

        /*public override void Use(GameTime gameTime, MouseState mState){
            if(!canAttack){
                return;
            }

            canAttack = false;

            Vector2 direction = mState.Position.ToVector2() - GameManager.Instance.entities[0].position;
            if(direction != Vector2.Zero){
                direction.Normalize();
                Vector2 spawnPos = GameManager.Instance.entities[0].position + direction * 10f;
                BaseProjectile bullet = new BaseProjectile(spawnPos, direction, 50, false);
                GameManager.Instance.AddProjectile(bullet);
            }
        }*/


        public override void BeginAttack()
        {
            if(owner == null || !isAttacking){
                return;
            }

            canAttack = false;
            isAttacking = true;
            attackTimer = 0f;
            cooldownTimer = TimeSpan.Zero;
            

            Vector2 targetDir;

            if (owner is Player)
            {
                var mouse = Mouse.GetState();
                targetDir = mouse.Position.ToVector2() - owner.position;
            }
            else
            {
                // For enemies: shoot toward the player
                targetDir = GameManager.Instance.player.position - owner.position;
            }

            if (targetDir != Vector2.Zero)
            {
                rotation = (float)Math.Atan2(targetDir.Y, targetDir.X);
                targetDir.Normalize();
                Vector2 spawnPos = owner.position + targetDir * 10f;
                bool enemyProjectile;
                if(owner is Player){
                    enemyProjectile = false;
                }
                else{
                    enemyProjectile = true;
                }
                
                BaseProjectile bullet = new BaseProjectile(spawnPos, targetDir, damage, enemyProjectile);
                GameManager.Instance.AddProjectile(bullet);
            }
        }
        public override void ContinueAttack(GameTime gameTime)
        {
            if (owner == null){
                return;
            }

            Vector2 direction = (owner is Player) ? Mouse.GetState().Position.ToVector2() - owner.position : GameManager.Instance.player.position - owner.position;

            rotation = (float)Math.Atan2(direction.Y, direction.X);
            if((MathHelper.PiOver2 < rotation) || (rotation < -MathHelper.PiOver2)){
                spriteEffects |= SpriteEffects.FlipVertically;
            }
            direction.Normalize();
            position = owner.position + direction * 20f;
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (owner == null){
                return;
            }
                

            Vector2 direction = (owner is Player) ? Mouse.GetState().Position.ToVector2() - owner.position : GameManager.Instance.player.position - owner.position;
            

            rotation = (float)Math.Atan2(direction.Y, direction.X);
            if((MathHelper.PiOver2 < rotation) || (rotation < -MathHelper.PiOver2)){
                spriteEffects |= SpriteEffects.FlipVertically;
            }

            spriteBatch.Draw(texture, owner.position, null, Color.White, rotation, rectangleSize, scale, spriteEffects, 0f);
        }
    }
}