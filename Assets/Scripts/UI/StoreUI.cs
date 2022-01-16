using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlienArena.Inventory;
using AlienArena.Itens;
using AlienArena.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.Store
{
    public class StoreUI : MonoBehaviour
    { 
        [SerializeField] private ItemSlot itemSlotPrefab;
        [SerializeField] private Transform storeContent;
        [SerializeField] private DescriptionUI descriptionUI;

        private Store _openedStore;
        private Player.Player _actualPlayer;
        
        private ItemSlot _selectedItemSlot;
        private List<ItemSlot> _itemSlotList = new List<ItemSlot>();

        private void Start()
        {
            UIController.instance.onStoreOpen += OpenStore;
        }

        public void BuyItem()
        {
            if(_selectedItemSlot == null) return;
            
            _openedStore.BuyItem(_selectedItemSlot.StoredItem, _actualPlayer);
        }

        public void OpenStore(Store store, Player.Player player)
        {
            foreach (var item in store.GetItensForSale())
            {
                AddedItem(item);
            }
        }

        public void AddedItem(Item item)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, storeContent);
            slot.SetItem(item, SetSelectedSlot);
            _itemSlotList.Add(slot);
        }

        public void RemovedItem(Item item)
        {
            ItemSlot slot = _itemSlotList.Find(x => x.StoredItem == item);
            _itemSlotList.Remove(slot);
            Destroy(slot);
        }

        public void SetSelectedSlot(ItemSlot slot)
        {
            _selectedItemSlot = slot;
            descriptionUI.SetDescription(slot.StoredItem);
        }

        public void ClearSelectedSlot()
        {
            _selectedItemSlot = null;
            descriptionUI.ClearDescription();
        }
    }
}