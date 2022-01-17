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
        
        private InventoryController _inventoryController;
        private GamePauseUIController _controller;
        private Store.Store _store;

        public void SellItem()
        {
            if (_selectedSlot == null) return;
            
            _store.SellItem(_selectedSlot.StoredItem, _inventoryController.ActualPlayer);
            _selectedSlot = null;
            descriptionUI.ClearDescription();
        }
        
        public void Equip()
        {
            _inventoryController.Equip(_selectedSlot.StoredItem);
            _selectedSlot = null;
            descriptionUI.ClearDescription();
        }
        
        private void Start()
        {
            _inventoryController = InventoryController.instance;
            _controller = GamePauseUIController.instance;
            _inventoryController.onInventoryChanged += InventoryChanged;
            _controller.onInventoryOpen += InventoryOpened;
            FillInventoryUI();
        }

        private void InventoryOpened(Store.Store store)
        {
            equipButton.gameObject.SetActive(store == null);
            sellButton.gameObject.SetActive(store != null);

            descriptionUI.ClearDescription();
            if (_selectedSlot != null)
            {
                _selectedSlot.UnSetSelection();
                _selectedSlot = null;
            }
                

            _store = store;
        }

        private void FillInventoryUI()
        {
            foreach (var item in _inventoryController.GetInventory().GetItemList())
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
            if(item == null) return;
            
            ItemSlot slot = Instantiate(itemSlotPrefab, itemSlotsContent);
            slot.SetItem(item, ClickedOnSlot);
            _itemSlotList.Add(slot);
        }

        private void RemovedItem(Item item)
        {
            if(item == null) return;
            
            ItemSlot slot = _itemSlotList.Find(x => x.StoredItem == item);
            if(slot == null) return;
            
            _itemSlotList.Remove(slot);
            Destroy(slot.gameObject);
        }

        private void ClickedOnSlot(ItemSlot slot)
        {
            if(_selectedSlot != null)
                _selectedSlot.UnSetSelection();
            
            slot.SetSelection();
            
            _selectedSlot = slot;
            descriptionUI.SetDescription(slot.StoredItem);
        }

    }
}