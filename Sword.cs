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
        float scale = 0.05f;
        float rotation;
        Vector2 direction;
        Vector2 hitBoxCenter;

        public Sword() : base("Sword", 15, 1.5f, FileManager.GetTexture("Sword"), 0, 0.5f){
            swordWidth = texture.Width * scale;
            swordHeight = texture.Height * scale;
        }


        public override void Use(GameTime gameTime, MouseState mState){
            if(!canAttack){
                return;
            }
            canAttack = false;
            
            
            //Vector2 mousePosition = mState.Position.ToVector2();
            direction = mState.Position.ToVector2() - GameManager.Instance.entities[0].position;
                   
            //Vector2 center = new Vector2(swordHitBox.X + swordHitBox.Width / 2, swordHitBox.Y + swordHitBox.Height / 2);


            if (direction == Vector2.Zero)
            {
                return;
            }

            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);  
            hitBoxCenter = GameManager.Instance.entities[0].position + direction * (swordHeight/2);

            Vector2 perp = new Vector2(-direction.Y, direction.X);
            Vector2 forward = direction * (swordHeight/2);
            Vector2 side = perp * (swordWidth/2);

            Vector2 topLeft = hitBoxCenter - forward - side;
            Vector2 topRight = hitBoxCenter - forward + side;
            Vector2 bottomLeft = hitBoxCenter + forward - side;
            Vector2 bottomRight = hitBoxCenter + forward + side;

            //Vector2 swingPos = player.position + direction * 30f;

            //Rectangle swordHitBox = new Rectangle(((int)swingPos.X - (int)(swordWidth / 2)), ((int)swingPos.Y - (int)(swordHeight / 2)), (int)swordWidth, (int)swordHeight);

            //Vector2 topLeft = player.position + new Vector2(-swordWidth / 2, -swordHeight / 2);
            //Vector2 topRight = player.position + new Vector2(swordWidth / 2, -swordHeight / 2);
            //Vector2 bottomLeft = player.position + new Vector2(-swordWidth / 2, swordHeight / 2);
            //Vector2 bottomRight = player.position + new Vector2(swordWidth / 2, swordHeight / 2);

            // Rotate all four corners based on the sword's rotation
            //topLeft = GameManager.Instance.RotatePoint(topLeft, player.position, rotation);
            //topRight = GameManager.Instance.RotatePoint(topRight, player.position, rotation);
            //bottomLeft = GameManager.Instance.RotatePoint(bottomLeft, player.position, rotation);
            //bottomRight = GameManager.Instance.RotatePoint(bottomRight, player.position, rotation);

            Vector2[] swordHitBoxVertices = new Vector2[]{topLeft, topRight, bottomRight, bottomLeft};
            
            foreach(var entity in GameManager.Instance.entities){
                if(entity is BaseEnemy enemy && enemy.isActive){
                    if(GameManager.Instance.IsPointInsidePolygon(enemy.hitBox, swordHitBoxVertices)){
                        enemy.TakeDamage(damage);
                    }
                }          
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //if (isActive)
            //{
            hitBoxCenter = GameManager.Instance.entities[0].position + direction * (swordHeight/2);
                spriteBatch.Draw(texture, hitBoxCenter, null, Color.White, (rotation+(float)Math.PI*1f/4f), new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            //}
        }
    }
}