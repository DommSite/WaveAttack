using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public abstract class Weapon
    {
        protected TimeSpan cooldownTimer = TimeSpan.Zero;
        protected HashSet<BaseEnemy> hitEnemies = new HashSet<BaseEnemy>();
        protected bool canAttack = true;
        protected bool isAttacking = false;
        protected float attackTimer = 0f;
        public Texture2D texture{get;}
        protected string name { get; }
        protected int damage { get; }
        protected float attackSpeed { get; }
        public int weaponNumber{get;}
        protected TimeSpan cooldownTime;   
        protected float attackDuration;
        protected Vector2 direction;
        protected Vector2[] swordHitBoxVertices;
        protected float rotation;
        protected float scale;
        protected float swordHeight;
        protected float swordWidth;



        public Weapon(string name, int damage, float attackSpeed, Texture2D texture, int weaponNumber, float scale, float attackDuration) 
        {
            this.name = name;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.texture = texture;
            this.weaponNumber = weaponNumber;
            this.scale = scale;
            this.attackDuration = attackDuration;
            this.cooldownTime = TimeSpan.FromSeconds(attackDuration*1.5);


            swordWidth = texture.Width * scale;
            swordHeight = texture.Height * scale;
        }

        public virtual void Use(GameTime gameTime, MouseState mState){
            if(!canAttack || isAttacking){
                return;
            }
            canAttack = false;
            isAttacking = true;
            attackTimer = 0f; 
            cooldownTimer = TimeSpan.Zero;     
            
            
            direction = mState.Position.ToVector2() - GameManager.Instance.entities[0].position;

            if (direction == Vector2.Zero)
            {
                canAttack = true;
                isAttacking = false;
                return;
            }

            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);  
            BeginAttack();
        }
        

        public virtual void Update(GameTime gameTime){
            if (isAttacking)
            {
                ContinueAttack(gameTime);

                attackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (attackTimer >= attackDuration)
                {
                    isAttacking = false;
                    cooldownTimer = TimeSpan.Zero;
                }

                return;
            }

            if (!canAttack)
            {
                cooldownTimer += gameTime.ElapsedGameTime;
                if (cooldownTimer >= cooldownTime)
                {
                    canAttack = true;
                }
            }
            
            /*if(!canAttack){
                cooldownTimer += gameTime.ElapsedGameTime;
                if(cooldownTimer >= cooldownTime){
                    isAttacking = false;
                    canAttack = true;
                    return;
                }
                return;
            }
            
            
            ContinueAttack(gameTime);
            /*if(cooldownTimer.TotalSeconds >= cooldownTime){
                canAttack = true;
                cooldownTimer = TimeSpan.Zero;
            }*/
        }
        

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }
        
        public abstract void BeginAttack();
        public abstract void ContinueAttack(GameTime gameTime);

        
    }
}