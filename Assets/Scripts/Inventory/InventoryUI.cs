using System;
using System.Collections.Generic;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private ItemSlot itemSlotPrefab;
        [SerializeField] private Transform itemSlotsContent;

        private List<ItemSlot> _itemSlotList = new List<ItemSlot>();
        private Inventory _inventory;

        private void Start()
        {
            _inventory = Inventory.instance;
            _inventory.onInventoryChanged += InventoryChanged;
            FillInventoryUI();
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
            Destroy(slot);
        }

        private void ClickedOnSlot(ItemSlot slot)
        {
            _inventory.Equip(slot.StoredItem);
        }
        
    }
}