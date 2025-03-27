using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class Weapon
    {
    protected Texture2D texture;
    protected string name { get; }
    protected int damage { get; }
    protected float attackSpeed { get; }

    public Weapon(string name, int damage, float attackSpeed, Texture2D texture)
    {
        this.name = name;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.texture = texture;
    }

    public abstract void Use(Player player);
    }
}