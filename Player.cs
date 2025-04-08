using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Player : BaseEntity
    {
        public int stamina = 100;
        public  Weapon currentWeapon {get;private set;}
        public int selectedWeaponSlot{get; private set;}
        private List<Weapon> inventory = new List<Weapon>();
        private MouseState oldState;
        private float runSpeed;
        private TimeSpan regenCooldown = TimeSpan.FromSeconds(2);
        private TimeSpan timeSpan = TimeSpan.Zero;
        private bool staminaEmpty = false;

        public List<Weapon> GetWeapons(){
            return inventory;
        }
    
        public Player(Vector2 position) : base(FileManager.GetTexture("Player"), position, 100, 1f){
            inventory.Add(new Sword());
            inventory.Add(new BigSword());
            inventory.Add(new Flintlock());

            currentWeapon = inventory[0];
            selectedWeaponSlot = 0;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mState = Mouse.GetState();
            KeyboardState kState = Keyboard.GetState();

            Move(gameTime);
            if(mState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released) {
                Attack(gameTime);
            }
            ChangeWeapon(kState);
        }

        public void ChangeWeapon(KeyboardState kState){
            for (int i = 0; i < 10; i++){
                Keys key = Keys.D1 + i;

                if (kState.IsKeyDown(key))
                {
                    if (i < inventory.Count && inventory[i] != null)
                    {
                        currentWeapon = inventory[i];
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
            currentWeapon.Use(this);
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
            if(kState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift)) 
            {
                if(stamina > 0 && direction !=Vector2.Zero && !staminaEmpty){
                    stamina--;
                    runSpeed = speed*2;
                } 
            }
            if(stamina <= 0 && !staminaEmpty){
                staminaEmpty = true;
                timeSpan = TimeSpan.Zero;
            }
            if(staminaEmpty){
                timeSpan += gameTime.ElapsedGameTime;
                
                if(timeSpan >= regenCooldown){
                    staminaEmpty = false;
                    timeSpan = TimeSpan.Zero;
                    stamina = 1;
                }
            }
            else if(stamina < 100){              
                timeSpan += gameTime.ElapsedGameTime;
                if (timeSpan >= TimeSpan.FromMilliseconds(100))
                {
                    stamina++;
                    timeSpan = TimeSpan.Zero;
                }
            }



            if(direction !=Vector2.Zero){
                direction.Normalize();
            }
            position += direction*runSpeed;        
            
        }


        public override void Die(){
            //you ded boi
        }

        public override void Draw(SpriteBatch spriteBatch){
            if (isActive)
            {
                spriteBatch.Draw(texture, position, null, Color.White, 0, new Vector2(texture.Width / 2, texture.Height / 2), 0.02f, SpriteEffects.None, 0f);
            }
        }

        

    }
}
