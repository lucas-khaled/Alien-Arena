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
        
        private int testIndex = -1;
        
        private bool _relationFilled = false;

        public EquipSlot GetSlotByType(Type type)
        {
            return slots.Find(x => x.data.type == type);
        }

        public Item Equip(Item item)
        {
            Item returnedItem = null;
            if(!_relationFilled) FillRelation();

            EquipSlot rightSlot = slots.Find(x => x.data.type == item.GetType());
            
            if (rightSlot.data.item != null) returnedItem = Unequip(rightSlot);

            item.HandleEquip(rightSlot);

            return returnedItem;
        }

        public Item Unequip(EquipSlot slot)
        {
            Item inItem = slot.data.item;
            
            slot.data.item = null;
            slot.spriteRenderer.sprite = null;
            slot.spriteRenderer.material = null;

            return inItem;
        }

        private void Awake()
        {
            FillRelation();
        }

        private void Start()
        {
            foreach (var slot in slots)
            {
                Equip(slot.data.item);

                if (slot.data.item.GetType() == typeof(Armor) || slot.data.item.GetType().IsSubclassOf(typeof(Armor)))
                {
                    Armor armor = (Armor) slot.data.item;
                    armor.HandlePlayerStats(Player.Player.instance);
                    //Debug.Log("Item: "+armor.name + " - " + armor.life);
                }
            }
        }

        private void FillRelation()
        {
            foreach(var type in Assembly.GetAssembly(typeof(Item)).GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Item))))
            {
                EquipSlot slot = slots.Find(x => x.spriteRenderer.name.Contains(type.Name));
                
                if(slot != null)
                    slot.data.type = type;
                
            }

            _relationFilled = true;
        }
    }

    [System.Serializable]
    public class EquipSlot
    {
        public SpriteRenderer spriteRenderer;
        public EquipData data;
    }
}
