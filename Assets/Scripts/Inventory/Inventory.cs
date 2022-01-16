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
        
        private List<Item> _itemsList = new List<Item>();
        private Equipper _equipper;
        
        public void AddItem(Item item)
        {
            _itemsList.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _itemsList.Remove(item);
        }

        public void Equip(Item item)
        {
            Item returnedItem = _equipper.Equip(item);
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
        }
    }
}
