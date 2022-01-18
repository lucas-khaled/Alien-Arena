using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlienArena.Inventory;
using AlienArena.Itens;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.UI
{
    public class EquipUI : MonoBehaviour
    {
        [SerializeField] private List<EquipSlotUI> slots;
        private InventoryController _inventory;

        private void Start()
        {
            _inventory = InventoryController.instance;
            _inventory.onEquip += OnEquip;
            
            FillRelation();
            InitEquipmentUI();
        }

        private void InitEquipmentUI()
        {
            foreach (var slot in slots)
            {
                slot.image.sprite = slot.data.item.sprite;
                slot.image.preserveAspect = true;
            }
        }

        private void OnEquip(Item equipped, Item unequipped)
        {
            EquipSlotUI slot = slots.Find(x => x.data.type == equipped.GetType());
            slot.image.sprite = equipped.sprite;
        }
        
        private void FillRelation()
        {
            foreach(var type in Assembly.GetAssembly(typeof(Item)).GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Item))))
            {
                EquipSlotUI slot = slots.Find(x => x.image.name.Contains(type.Name));
                
                if(slot != null)
                    slot.data.type = type;
            }
        }
    }

    [System.Serializable]
    public class EquipSlotUI
    {
        public Image image;
        public EquipData data;
    }
}
