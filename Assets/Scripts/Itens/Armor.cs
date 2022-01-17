using AlienArena.Inventory;
using UnityEngine;

namespace AlienArena.Itens
{
    public class Armor : Item
    {
        [Header("Armor data")]
        public float life;
        public float velocity;
        public float energy;
        
        public override void HandleEquip(EquipSlot slot)
        {
            Base_HandleEquip(slot);
        }

        public virtual void HandlePlayerStats(Player.Player player, int multiplyer = 1)
        {
            player.AddLife(life * multiplyer);
            player.AddMaxEnergy(energy * multiplyer);
            player.AddVelocity(velocity * multiplyer);
        }
    }
}