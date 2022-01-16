using System;
using UnityEngine;
using AlienArena.Inventory;
using AlienArena.Itens;

namespace AlienArena.Player
{
    public class Player : MonoBehaviour
    {
        public int Coins { get; private set; }
        public float Life { get; private set; }
        public float Velocity { get; private set; }
        public float Energy { get; private set; }

        public Action<float, string> onChangeAtrribute;

        private void Start()
        {
            Inventory.Inventory.instance.onEquip += ChangeEquipStats;
        }

        public void AddCoins(int coins)
        {
            Coins += coins;
            onChangeAtrribute?.Invoke(Coins, "Coins");
        }

        public void AddLife(float life)
        {
            Life += life;
            onChangeAtrribute?.Invoke(Life, "Life");
        }

        public void AddVelocity(float velocity)
        {
            Velocity += velocity;
            onChangeAtrribute?.Invoke(Life, "Velocity");
        }

        public void AddEnergy(float energy)
        {
            Energy += energy;
            onChangeAtrribute?.Invoke(Life, "Energy");
        }

        public void ChangeEquipStats(Item addedItem, Item removedItem)
        {
            if (!addedItem.GetType().IsSubclassOf(typeof(Armor))) return;

            Armor armorAdded = (Armor) addedItem;
            armorAdded.HandlePlayerStats(this);
            
            if(removedItem == null) return;

            Armor removedArmor = (Armor) removedItem;
            removedArmor.HandlePlayerStats(this);
        }
        
    }
}