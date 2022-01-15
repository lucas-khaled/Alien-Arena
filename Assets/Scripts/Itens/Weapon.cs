using AlienArena;
using AlienArena.Inventory;
using UnityEngine;

namespace AlienArena.Itens
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Itens/Weapon")]
    public class Weapon : Item
    {
        public float damage;
        public Projectile projectile;
        
        public override void HandleEquip(EquipSlot slot)
        {
            Base_HandleEquip(slot);
        }
    }
}