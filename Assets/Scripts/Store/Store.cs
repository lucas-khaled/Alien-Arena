using System;
using System.Collections.Generic;
using AlienArena.Itens;
using UnityEditor;
using UnityEngine;

namespace AlienArena.Store
{
    [CreateAssetMenu(fileName = "Store", menuName = "Alien Arena/Store")]
    public class Store : ScriptableObject
    {
        [SerializeField] private List<Item> itensForSale;

        public Action<Item, bool> onStoreChanged;
        public Action<Item> onInsuficientCoins;

        public void BuyItem(Item item, Player.Player player)
        {
            if (player.PlayerStats.coins < item.price)
            {
                onInsuficientCoins?.Invoke(item);
                return;
            }
            
            itensForSale.Remove(item);
            player.AddCoins(-item.price);
            Inventory.InventoryController.instance.AddItem(item);
            
            onStoreChanged?.Invoke(item, false);
        }

        public void SellItem(Item item, Player.Player player)
        {
            itensForSale.Add(item);
            player.AddCoins(item.price);
            Inventory.InventoryController.instance.RemoveItem(item);
            
            onStoreChanged?.Invoke(item, true);
        }

        public List<Item> GetItensForSale()
        {
            return itensForSale;
        }
        
        #if UNITY_EDITOR
        [ContextMenu("Find All Items")]
        private void FindItems()
        {
            itensForSale.Clear();
            string[] guids = AssetDatabase.FindAssets("t: Item");
            foreach (var guid in guids)
            {
                Item item = AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid));
                itensForSale.Add(item);
            }
        }
        #endif
    }
}