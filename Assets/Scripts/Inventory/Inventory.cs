using System.Collections.Generic;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Inventory
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Alien Arena/Inventory")]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private List<Item> _itemsList;
        
        public void AddItem(Item item)
        {
            _itemsList.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _itemsList.Remove(item);
        }
        
        public List<Item> GetItemList()
        {
            return _itemsList;
        }
    }
}