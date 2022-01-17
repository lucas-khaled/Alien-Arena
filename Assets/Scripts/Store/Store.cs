using System;
using System.Collections.Generic;
using AlienArena.Itens;
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
    }
}