using System;
using System.Collections.Generic;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;

        public Action<Item, Item> onEquip;
        public Action<Item, bool> onInventoryChanged;
        
        public Player.Player ActualPlayer { get; private set; }

        [SerializeField] private List<Item> _itemsList; //= new List<Item>();
        private Equipper _equipper;
        

        public List<Item> GetItemList()
        {
            return _itemsList;
        }
        
        
        public void AddItem(Item item)
        {
            if(item == null) return;
            
            _itemsList.Add(item);
            onInventoryChanged?.Invoke(item, true);
        }

        public void RemoveItem(Item item)
        {
            if(item == null) return;
            
            _itemsList.Remove(item);
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
            ActualPlayer = FindObjectOfType<Player.Player>();
        }
    }
}
