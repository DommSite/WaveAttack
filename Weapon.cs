using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public abstract class Weapon
    {
        public Texture2D texture{get;}
        protected string name { get; }
        protected int damage { get; }
        protected float attackSpeed { get; }
        public int weaponNumber{get;}
        protected float cooldownTime;
        protected TimeSpan cooldownTimer = TimeSpan.Zero;
        protected bool canAttack = true;

        public Weapon(string name, int damage, float attackSpeed, Texture2D texture, int weaponNumber, float cooldownTime)
        {
            this.name = name;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.texture = texture;
            this.weaponNumber = weaponNumber;
            this.cooldownTime = cooldownTime;
        }

        public abstract void Use(GameTime gameTime, MouseState mState);
        

        public virtual void Update(GameTime gameTime){
            if(!canAttack){
                cooldownTimer += gameTime.ElapsedGameTime;
                if(cooldownTimer.TotalSeconds >= cooldownTime){
                    canAttack = true;
                    cooldownTimer = TimeSpan.Zero;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //if (isActive)
            //{
                //spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            //}
        }
            
        
    }
}