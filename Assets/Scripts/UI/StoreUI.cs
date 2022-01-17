using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlienArena.Inventory;
using AlienArena.Itens;
using AlienArena.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.Store
{
    public class StoreUI : MonoBehaviour
    { 
        [SerializeField] private ItemSlot itemSlotPrefab;
        [SerializeField] private Transform storeContent;
        [SerializeField] private Transform insuficientCoinsPanel;
        [SerializeField] private DescriptionUI descriptionUI;

        private Store _openedStore;
        private Player.Player _actualPlayer;
        
        private ItemSlot _selectedItemSlot;
        private List<ItemSlot> _itemSlotList = new List<ItemSlot>();

        public void BuyItem()
        {
            if(_selectedItemSlot == null) return;
            
            _openedStore.BuyItem(_selectedItemSlot.StoredItem, _actualPlayer);
            _selectedItemSlot = null;
            descriptionUI.ClearDescription();
        }

        public void OpenStore(Store store, Player.Player player)
        {
            descriptionUI.ClearDescription();
            
            if (_selectedItemSlot != null)
            {
                _selectedItemSlot.UnSetSelection();
                _selectedItemSlot = null;
            }
            
            if(store == _openedStore) return;
            
            ClearStore();
            foreach (var item in store.GetItensForSale())
            {
                AddedItem(item);
            }

            if (_openedStore != null)
            {
                _openedStore.onStoreChanged -= StoreChanged;
                _openedStore.onInsuficientCoins -= InsuficientCoins;
            }

            store.onStoreChanged += StoreChanged;
            store.onInsuficientCoins += InsuficientCoins;
            
            _openedStore = store;
            _actualPlayer = player;
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
            Destroy(slot.gameObject);
        }

        public void SetSelectedSlot(ItemSlot slot)
        {
            if(_selectedItemSlot != null)
                _selectedItemSlot.UnSetSelection();
            
            slot.SetSelection();
            
            _selectedItemSlot = slot;
            descriptionUI.SetDescription(slot.StoredItem);
        }

        private void InsuficientCoins(Item item)
        {
            Sequence showPanel = DOTween.Sequence();
            showPanel.Append(insuficientCoinsPanel.DOMoveX(100, 0.3f)).AppendInterval(1)
                .Append(insuficientCoinsPanel.DOMoveX(-100, 0.5f)).SetUpdate(true);
        }

        private void StoreChanged(Item item, bool added)
        {
            if(added)
                AddedItem(item);
            else
                RemovedItem(item);
        }

        private void ClearStore()
        {
            foreach (var item in _itemSlotList)
                Destroy(item.gameObject);
            
            _itemSlotList.Clear();
        }
        
        private void Start()
        {
            GamePauseUIController.instance.onStoreOpen += OpenStore;
        }
    }
}