using AlienArena.Inventory;
using UnityEngine;

namespace AlienArena.Itens
{
    public class Armor : Item
    {
        public float life;
        public float velocity;
        public float energy;
        
        public override void HandleEquip(EquipSlot slot)
        {
            Base_HandleEquip(slot);
        }
    }
}