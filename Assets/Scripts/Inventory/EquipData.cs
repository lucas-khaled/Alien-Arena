using System;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Inventory
{
    [CreateAssetMenu(fileName = "Equip Data", menuName = "Alien Arena/Equip Data", order = 0)]
    public class EquipData : ScriptableObject
    {
        public Item item;
        public Type type { get; set; }
    }
}