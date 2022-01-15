using AlienArena;
using UnityEngine;

namespace AlienArena.Itens
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "AlienArena/Itens/Weapon")]
    public class Weapon : Item
    {
        public float damage;
        public Projectile projectile;
        
        public override void HandleEquip(SpriteRenderer renderer)
        {
            Base_HandleEquip(renderer);
        }
    }
}