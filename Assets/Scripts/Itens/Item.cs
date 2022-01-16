using AlienArena.Inventory;
using UnityEngine;

namespace AlienArena.Itens
{
    public abstract class Item : ScriptableObject
    {
        [Header("Item data")]
        public Sprite sprite;
        public Material material;
        public int price;
    
        public abstract void HandleEquip(EquipSlot slot);

        protected void Base_HandleEquip(EquipSlot slot)
        {
            slot.item = this;
            slot.spriteRenderer.sprite = sprite;
            slot.spriteRenderer.material = material;
        } 
    }
}
