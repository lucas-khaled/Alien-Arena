using AlienArena;
using AlienArena.Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace AlienArena.Itens
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Alien Arena/Itens/Weapon")]
    public class Weapon : Item
    {
        [Header("Weapon Data")]
        public float damage;
        [FormerlySerializedAs("projectileQuantity")] public float bullets = 1;
        public Projectile projectile;
        
        public override void HandleEquip(EquipSlot slot)
        {
            Base_HandleEquip(slot);
        }
    }
}