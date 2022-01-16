using System.Collections.Generic;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Store
{
    [System.Serializable]
    public class Store
    {
        [SerializeField] private List<Item> itensForSale;

        public void BuyItem(Item item, Player.Player player)
        {
            itensForSale.Remove(item);
            player.AddCoins(-item.price);
        }

        public void SellItem(Item item, Player.Player player)
        {
            itensForSale.Add(item);
            player.AddCoins(item.price);
        }
    }
}