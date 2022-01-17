using System;
using System.Collections.Generic;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public static InventoryController instance;

        public Action<Item, Item> onEquip;
        public Action<Item, bool> onInventoryChanged;
        
        public Player.Player ActualPlayer { get; private set; }

        [SerializeField] private Inventory inventory;
        private Equipper _equipper;
        

        public Inventory GetInventory()
        {
            return inventory;
        }
        
        
        public void AddItem(Item item)
        {
            if(item == null) return;
            
            inventory.AddItem(item);
            onInventoryChanged?.Invoke(item, true);
        }

        public void RemoveItem(Item item)
        {
            if(item == null) return;
            
            inventory.RemoveItem(item);
            onInventoryChanged?.Invoke(item, false);
        }

        public void Equip(Item item)
        {
            Item returnedItem = _equipper.Equip(item);
            
            RemoveItem(item);
            AddItem(returnedItem);
            
            onEquip?.Invoke(item, returnedItem);
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _equipper = FindObjectOfType<Equipper>();
            ActualPlayer = Player.Player.instance;
        }
    }
}
