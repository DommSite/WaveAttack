using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public class StandardEnemy : BaseEnemy
    {
        private Player player;

        public StandardEnemy(Texture2D texture, Vector2 position, Player player)
            : base(texture, position, 50, 2f)
        {
            this.player = player;
        }

        public override void Attack(Player player){
            //code do code
        }

        public override void Move(GameTime gameTime)
        {
            Vector2 direction = player.position - position;
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                position += direction * speed;
            }


        }
    }
}