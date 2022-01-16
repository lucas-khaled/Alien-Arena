using System;
using System.Collections.Generic;
using AlienArena.Itens;
using AlienArena.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private ItemSlot itemSlotPrefab;
        [SerializeField] private Transform itemSlotsContent;
        [SerializeField] private Button equipButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private DescriptionUI descriptionUI;

        private List<ItemSlot> _itemSlotList = new List<ItemSlot>();
        private ItemSlot _selectedSlot;
        
        private Inventory _inventory;
        private UIController _controller;
        private Store.Store _store;

        public void SellItem()
        {
            if (_selectedSlot == null) return;
            
            _store.SellItem(_selectedSlot.StoredItem, _inventory.ActualPlayer);
        }
        
        public void Equip()
        {
            _inventory.Equip(_selectedSlot.StoredItem);
        }
        
        private void Start()
        {
            _inventory = Inventory.instance;
            _controller = UIController.instance;
            _inventory.onInventoryChanged += InventoryChanged;
            _controller.onInventoryOpen += InventoryOpened;
            FillInventoryUI();
        }

        private void InventoryOpened(Store.Store store)
        {
            equipButton.gameObject.SetActive(store == null);
            sellButton.gameObject.SetActive(store != null);

            _store = store;
        }

        private void FillInventoryUI()
        {
            foreach (var item in _inventory.GetItemList())
                AddedItem(item);
        }

        private void InventoryChanged(Item item, bool added)
        {
            if(added)
                AddedItem(item);
            else
                RemovedItem(item);
        }

        private void AddedItem(Item item)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, itemSlotsContent);
            slot.SetItem(item, ClickedOnSlot);
            _itemSlotList.Add(slot);
        }

        private void RemovedItem(Item item)
        {
            ItemSlot slot = _itemSlotList.Find(x => x.StoredItem == item);
            _itemSlotList.Remove(slot);
            Destroy(slot.gameObject);
        }

        private void ClickedOnSlot(ItemSlot slot)
        {
            _selectedSlot = slot;
            descriptionUI.SetDescription(slot.StoredItem);
        }

    }
}