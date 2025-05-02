using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SharpFont.Cache;
using WaveAttack.Weapons;

namespace WaveAttack.Entities
{
    public abstract class BaseEntity : BaseClass
    {
        public int health{get; protected set;}
        protected float speed;
        public  Weapon weapon {get; protected set;}
        protected float repulsionStrength = 0.2f;
        protected SoundEffect hurt;
        protected float volume = 1;
        protected float pitch = 1;
        protected float pan = 1;

        public BaseEntity(Texture2D texture, Vector2 position, float scale, int health, float speed, float volume) : base(texture, position, scale){
            this.health = health;
            this.speed = speed;
            this.volume = volume;
            hurt = FileManager.GetSound("Hurt1Retro");
        }
        public abstract void Die();
        public abstract void Move(GameTime gameTime);
        public abstract void Attack(GameTime gameTime);

        public void TakeDamage(int damage){
            health -= damage;
            if(!GameManager.Instance.settings.MuteAll){
                hurt.Play(volume * GameManager.Instance.settings.Volume * GameManager.Instance.settings.SFXVolume, pitch, pan);
            }
            
            if(health <= 0){
                health = 0;
                Die();
            }
        }
        
        public void ApplyRepulsion()
        {
            float repulsionRadius = Math.Max(texture.Width, texture.Height) * scale * 0.6f;

            foreach (var entity in GameManager.Instance.entities)
            {
                if (entity is BaseEntity other && other != this && other.isActive)
                {
                    Vector2 diff = position - other.position;
                    float distance = diff.Length();

                    if (distance < repulsionRadius && distance > 0.01f) // prevent divide by zero
                    {
                        Vector2 pushDir = diff / distance;
                        float pushAmount = (repulsionRadius - distance) * repulsionStrength;
                        position += pushDir * pushAmount;
                    }
                }
            }
        }


    }
}