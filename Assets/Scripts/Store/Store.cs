using System;
using System.Collections.Generic;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Store
{
    [System.Serializable]
    public class Store
    {
        [SerializeField] private List<Item> itensForSale;

        public Action<Item, bool> onStoreChanged;

        public void BuyItem(Item item, Player.Player player)
        {
            itensForSale.Remove(item);
            player.AddCoins(-item.price);
            Inventory.Inventory.instance.AddItem(item);
            
            onStoreChanged?.Invoke(item, false);
        }

        public void SellItem(Item item, Player.Player player)
        {
            itensForSale.Add(item);
            player.AddCoins(item.price);
            Inventory.Inventory.instance.RemoveItem(item);
            
            onStoreChanged?.Invoke(item, true);
        }

        public List<Item> GetItensForSale()
        {
            return itensForSale;
        }
    }
}