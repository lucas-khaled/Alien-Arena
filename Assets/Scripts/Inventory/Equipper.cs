using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Inventory
{
    public class Equipper : MonoBehaviour
    {
        [SerializeField] private List<EquipSlot> slots;
        
        private bool _relationFilled = false;

        public Item Equip(Item item)
        {
            Item returnedItem = null;
            if(!_relationFilled) FillRelation();

            EquipSlot rightSlot = slots.Find(x => x.type == item.GetType());
            
            if (rightSlot.item != null) returnedItem = Unequip(rightSlot);

            item.HandleEquip(rightSlot);

            return returnedItem;
        }

        public Item Unequip(EquipSlot slot)
        {
            Item inItem = slot.item;
            
            slot.item = null;
            slot.spriteRenderer.sprite = null;
            slot.spriteRenderer.material = null;

            return inItem;
        }

        private void Awake()
        {
            FillRelation();
        }

        private void FillRelation()
        {
            foreach(var type in Assembly.GetAssembly(typeof(Item)).GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Item))))
            {
                Debug.Log(type.Name);
                EquipSlot slot = slots.Find(x => x.spriteRenderer.name.Contains(type.Name));
                
                if(slot != null)
                    slot.type = type;
                
            }

            _relationFilled = true;
        }
    }

    [System.Serializable]
    public class EquipSlot
    {
        public SpriteRenderer spriteRenderer;
        public Item item { get; set; }
        public Type type { get; set; }
        
    }
}
