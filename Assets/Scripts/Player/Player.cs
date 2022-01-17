using System;
using UnityEngine;
using AlienArena.Inventory;
using AlienArena.Itens;

namespace AlienArena.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        public Action<float, string> onChangeAtrribute;

        public PlayerStats PlayerStats => stats;
        
        private void Start()
        {
            Inventory.InventoryController.instance.onEquip += ChangeEquipStats;
            AddLife(100);
        }

        public void UseEnergy(float usage)
        {
            stats.energy -= usage;
            onChangeAtrribute?.Invoke(stats.energy,"Energy");
        }

        public void Damage(float damage)
        {
            stats.life = Mathf.Clamp(stats.life - damage, 0, stats.maxLife);
            onChangeAtrribute?.Invoke(stats.life, "Life");
        }

        public void AddCoins(int coins)
        {
            stats.coins += coins;
            onChangeAtrribute?.Invoke(stats.coins, "Coins");
        }

        public void AddLife(float life)
        {
            stats.maxLife += life;
            Damage(-life);
            onChangeAtrribute?.Invoke(stats.maxLife, "MaxLife");
        }

        public void AddVelocity(float velocity)
        {
            stats.velocity += velocity;
            onChangeAtrribute?.Invoke(stats.velocity, "Velocity");
        }

        public void AddMaxEnergy(float energy)
        {
            stats.maxEnergy += energy;
            UseEnergy(-energy);
            onChangeAtrribute?.Invoke(stats.maxEnergy, "MaxEnergy");
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