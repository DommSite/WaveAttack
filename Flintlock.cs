using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WaveAttack
{
    public class Flintlock : Weapon
    {
        public Flintlock() : base("Flintlock", 40, 2f, FileManager.GetTexture("Flintlock"),2, 1f){
        }

        public override void Use(GameTime gameTime, Player player){
            if(!canAttack){
                return;
            }

            canAttack = false;

            Vector2 direction = Mouse.GetState().Position.ToVector2() - player.position;
            if(direction != Vector2.Zero){
                direction.Normalize();
                Vector2 spawnPos = player.position + direction * 10f;
                BaseProjectile bullet = new BaseProjectile(spawnPos, direction, 50, false);
                GameManager.Instance.AddProjectile(bullet);
            }
        }
    }
}