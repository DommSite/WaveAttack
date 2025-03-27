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
        private Weapon currentWeapon;
        private List<Weapon> inventory = new List<Weapon>();
        
        private MouseState oldState;
        public Texture2D swordTexture;
        public Texture2D bigSwordTexture;
        public Texture2D gunTexture;
        public Texture2D bulletTexture;


        public Player(Texture2D texture, Vector2 position, Texture2D swordTexture, Texture2D bigSwordTexture, Texture2D gunTexture, Texture2D bulletTexture) : base(texture, position, 100, 5f){
            this.swordTexture = swordTexture;
            this.bigSwordTexture = bigSwordTexture;
            this.gunTexture = gunTexture;
            this.bulletTexture = bulletTexture;

            currentWeapon = new Sword(this.swordTexture);
            inventory.Add(currentWeapon);
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
            if (Keyboard.GetState().IsKeyDown(Keys.D1)){
                currentWeapon = inventory[0];
            }          
            if (Keyboard.GetState().IsKeyDown(Keys.D2) && inventory.Count > 1){
                currentWeapon = inventory[1];
            }            
        }

        public override void Attack(GameTime gameTime){
            currentWeapon.Use(this);
        }

        public override void Move(GameTime gameTime){
            KeyboardState kState = Keyboard.GetState();
            Vector2 direction = new Vector2(0,0);
            
            

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
                stamina--;
                if(stamina > 0){
                    speed = speed*2;
                }                       
            }
            else{
                if(stamina < 100){
                    stamina++;
                }
            }
            if(direction !=Vector2.Zero){
                direction.Normalize();
            }
            position += direction*speed;
        }

        public override void Die(){
            //you ded boi
        }

        

        

    }
}
