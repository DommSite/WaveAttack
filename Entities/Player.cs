using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WaveAttack.Weapons;

namespace WaveAttack.Entities
{
    public class Player : BaseEntity
    {
        public int stamina = 100;
        public int selectedWeaponSlot{get; private set;}
        private List<Weapon> inventory = new List<Weapon>();
        private MouseState oldState;
        private float runSpeed;
        private TimeSpan staminaDrainRate = TimeSpan.FromMilliseconds(100);
        private TimeSpan staminaRegenRate = TimeSpan.FromMilliseconds(100);
        private TimeSpan regenCooldown = TimeSpan.FromSeconds(2);
        private TimeSpan drainTimer = TimeSpan.Zero;
        private TimeSpan regenTimer = TimeSpan.Zero;
        private TimeSpan cooldownTimer = TimeSpan.Zero;
        private bool staminaEmpty = false;
        public int killCount = 0;

        public List<Weapon> GetWeapons(){
            return inventory;
        }
    
        public Player(Vector2 position) : base(FileManager.GetTexture("Player"), position, 0.02f, 100, 1f){
            inventory.Add(new Sword(this));
            inventory.Add(new BigSword(this));
            inventory.Add(new Flintlock(this));

            weapon = inventory[0];
            selectedWeaponSlot = 0;
        }
        public void test(){
            health++;
        }
        public void test2(){
            health++;
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            Attack(gameTime);          
            ChangeWeapon();
            weapon?.Update(gameTime);
            hitBox = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * scale), (int)(texture.Height * scale));
        }

        public void ChangeWeapon(){
            KeyboardState kState = Keyboard.GetState();
            for (int i = 0; i < 10; i++){
                Keys key = Keys.D1 + i;

                if (kState.IsKeyDown(key))
                {
                    if (i < inventory.Count && inventory[i] != null)
                    {
                        weapon = inventory[i];
                        selectedWeaponSlot = i;
                    }
                    else
                    {
                        // Optional: play a "fail" sound or show a message
                        //Console.WriteLine($"No weapon in slot {i + 1}");
                    }
                    break; // prevent multiple switches in one frame
                }
            }
            /*
            if (Keyboard.GetState().IsKeyDown(Keys.D1)){
                currentWeapon = inventory[0];
            }          
            if (Keyboard.GetState().IsKeyDown(Keys.D2) && inventory.Count > 1){
                currentWeapon = inventory[1];
            }    */        
        }

        public override void Attack(GameTime gameTime){
            MouseState mState = Mouse.GetState();
            if(mState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) {
                weapon?.Use(gameTime, mState.Position.ToVector2());
            }  
            oldState = mState;
        }

        public override void Move(GameTime gameTime){
            KeyboardState kState = Keyboard.GetState();
            Vector2 direction = new Vector2(0,0);
            runSpeed = speed;
            
            

            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W)) 
            {
                direction.Y -= 1;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S)) 
            {
                direction.Y += 1;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A)) 
            {
                direction.X -= 1;
            }
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D)) 
            {
                direction.X += 1;
            }
            if (kState.IsKeyDown(Keys.LeftShift) && direction != Vector2.Zero && !staminaEmpty)
            {
                drainTimer += gameTime.ElapsedGameTime;
                if (drainTimer >= staminaDrainRate)
                {
                    stamina--;
                    drainTimer = TimeSpan.Zero;

                    if (stamina <= 0)
                    {
                        staminaEmpty = true;
                        cooldownTimer = TimeSpan.Zero;
                    }
                }
                runSpeed = speed * 2;
            }
            else
            {
                // Not running — regen logic
                if (staminaEmpty)
                {
                    cooldownTimer += gameTime.ElapsedGameTime;
                    if (cooldownTimer >= regenCooldown)
                    {
                        staminaEmpty = false;
                        regenTimer = TimeSpan.Zero;
                        stamina = 1; // Optional: kickstart regen
                    }
                }
                else if (stamina < 100)
                {
                    regenTimer += gameTime.ElapsedGameTime;
                    if (regenTimer >= staminaRegenRate)
                    {
                        stamina++;
                        regenTimer = TimeSpan.Zero;
                    }
                }
            }



            if(direction !=Vector2.Zero){
                direction.Normalize();
            }
            ApplyRepulsion(); 
            position += direction*runSpeed;        
            
        }


        public override void Die(){
            //you ded boi
        }

       

        

    }
}
