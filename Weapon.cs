using System;
using System.Collections.Generic;
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
        protected HashSet<BaseEnemy> hitEnemies = new HashSet<BaseEnemy>();
        protected bool isAttacking = false;
        protected float slashTimer = 0f;
        protected Vector2 direction;
        protected Vector2[] swordHitBoxVertices;
        protected float rotation;
        protected float scale;



        public Weapon(string name, int damage, float attackSpeed, Texture2D texture, int weaponNumber, float cooldownTime) 
        {
            this.name = name;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.texture = texture;
            this.weaponNumber = weaponNumber;
            this.cooldownTime = cooldownTime;
        }

        public virtual void Use(GameTime gameTime, MouseState mState){
            if(!canAttack || isAttacking){
                return;
            }
            canAttack = false;
            isAttacking = true;
            slashTimer = 0f;      
            hitEnemies.Clear();   
           
            direction = mState.Position.ToVector2() - GameManager.Instance.entities[0].position;

            if (direction == Vector2.Zero)
            {
                return;
            }

            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);  
        }
        

        public virtual void Update(GameTime gameTime){
            if(!canAttack){
                cooldownTimer += gameTime.ElapsedGameTime;
                if(cooldownTimer.TotalSeconds >= cooldownTime){
                    canAttack = true;
                    cooldownTimer = TimeSpan.Zero;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //if (isActive)
            //{
                //spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0f);
            //}
        }
            
        
    }
}