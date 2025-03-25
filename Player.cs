using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Player : BaseEntity
    {
        public int stamina = 100;
        //private weapon currentWeapon;

        public Player(Texture2D texture, Vector2 position) : base(texture, position, 100, 5f){
            //currentWeapon = new Sword();
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
        
            //attacksaken här


            position += direction*speed;





        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);

        

        }

        

    }
}
