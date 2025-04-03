using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WaveAttack
{
    public abstract class Weapon
    {
    public Texture2D texture{get;}
    protected string name { get; }
    protected int damage { get; }
    protected float attackSpeed { get; }
    public int weaponNumber{get;}

    public Weapon(string name, int damage, float attackSpeed, Texture2D texture, int weaponNumber)
    {
        this.name = name;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.texture = texture;
        this.weaponNumber = weaponNumber;
    }

    public abstract void Use(Player player);
    }
}