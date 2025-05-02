using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using WaveAttack.Entities;

namespace WaveAttack.Weapons
{
    public abstract class Weapon : BaseClass
    {
        protected TimeSpan cooldownTimer = TimeSpan.Zero;
        protected HashSet<BaseEntity> hitTargets = new HashSet<BaseEntity>();
        protected bool canAttack = true;
        public bool isAttacking = false;
        protected float attackTimer = 0f;
        protected string name { get; }
        protected int damage { get; }
        protected float attackSpeed { get; }
        public int weaponNumber{get;}
        protected TimeSpan cooldownTime;   
        protected float attackDuration;
        protected Vector2 direction;
        //protected Vector2[] swordHitBoxVertices;
        
        
        protected float swordHeight;
        protected float swordWidth;
        protected BaseEntity owner;
        protected SoundEffect soundEffect;
        protected float volume;
        protected float pitch;
        protected float pan;



        public Weapon(string name, int damage, float attackSpeed, Texture2D texture, int weaponNumber, float scale, float attackDuration, BaseEntity owner, SoundEffect soundEffect, float volume, float pitch, float pan) : base(texture, Vector2.Zero, scale)
        {
            this.name = name;
            this.damage = damage;
            this.attackSpeed = attackSpeed;
            this.weaponNumber = weaponNumber;
            this.attackDuration = attackDuration;
            this.cooldownTime = TimeSpan.FromSeconds(attackDuration*1.5);
            this.owner = owner;
            this.soundEffect = soundEffect;
            this.volume = volume;
            this.pitch = pitch;
            this.pan = pan;


            if(owner is not Player){
                this.volume = volume/3;
            }
            swordWidth = texture.Width * scale;
            swordHeight = texture.Height * scale;
        }

        public virtual void Use(GameTime gameTime, Vector2 targetPosition){
            if(!canAttack || isAttacking){
                return;
            }
            canAttack = false;
            isAttacking = true;
            attackTimer = 0f; 
            cooldownTimer = TimeSpan.Zero;     
            
           
            direction = targetPosition - owner.position;
             

            if (direction == Vector2.Zero)
            {
                
                canAttack = true;
                isAttacking = false;
                return;
            }

            direction.Normalize();
            rotation = (float)Math.Atan2(direction.Y, direction.X);  

            BeginAttack();
            if(!GameManager.Instance.settings.MuteAll){
                soundEffect.Play(volume* GameManager.Instance.settings.Volume * GameManager.Instance.settings.SFXVolume, pitch, pan);
            }
            
        }
        

        public override void Update(GameTime gameTime){
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
        

        /*public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }*/
        
        public abstract void BeginAttack();
        public abstract void ContinueAttack(GameTime gameTime);

        
    }
}