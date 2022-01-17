using System;
using UnityEngine;
using AlienArena.Inventory;
using AlienArena.Itens;

namespace AlienArena.Player
{
    public class Player : MonoBehaviour
    {
        public static Player instance;
            
        [SerializeField] private PlayerStats stats;
        public Action<float, string> onChangeAtrribute;

        public PlayerStats PlayerStats => stats;

        private void Awake()
        {
            PlayerStats.Init();
            instance = this;
        }

        private void Start()
        {
            InventoryController.instance.onEquip += ChangeEquipStats;
            AddLife(100);
        }

        public void UseEnergy(float usage)
        {
            stats.energy -= usage;
            onChangeAtrribute?.Invoke(stats.energy,"Energy");
        }

        public void Damage(float damage)
        {
            stats.life = Mathf.Clamp(stats.life - damage, 0, stats.AddedMaxLife);
            onChangeAtrribute?.Invoke(stats.life, "Life");
        }

        public void AddCoins(int coins)
        {
            stats.coins += coins;
            onChangeAtrribute?.Invoke(stats.coins, "Coins");
        }

        public void AddLife(float life)
        {
            stats.AddedMaxLife += life;
            Damage(-life);
            onChangeAtrribute?.Invoke(stats.AddedMaxLife, "MaxLife");
        }

        public void AddVelocity(float velocity)
        {
            stats.AddedVelocity += velocity;
            onChangeAtrribute?.Invoke(stats.AddedVelocity, "Velocity");
        }

        public void AddMaxEnergy(float energy)
        {
            stats.AddedMaxEnergy += energy;
            UseEnergy(-energy);
            onChangeAtrribute?.Invoke(stats.AddedMaxEnergy, "MaxEnergy");
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