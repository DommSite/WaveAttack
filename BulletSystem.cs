using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class BulletSystem
    {
        public static BulletSystem Instance{
            get{
                return instance;
            }
        }
        private static BulletSystem instance;
        List<Bullet> bullets = new List<Bullet>();
        Texture2D bulletImage;

        public static void CreateInstance(Texture2D bulletImage){
            instance = new BulletSystem(bulletImage);
        }
        
        private BulletSystem(Texture2D bulletImage){
            this.bulletImage = bulletImage;
        }

        public void SummonBullet(Vector2 position, Vector2 direction){
            bullets.Add(new Bullet(bulletImage, position, direction));
        }

        private void RemoveBullet(){
            /*for (int i = 0;i<bullets.Count;i++){
                if(bullets[i].Position.Y>500){
                    particles.RemoveAt(i);
                    i--;
                }*/
            }      
        
        public virtual void Update(){
            foreach(var Bullet in bullets){
                Bullet.Update();
            }
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(var Bullet in bullets){
                Bullet.Draw(spriteBatch);
            }
        }
    }
}