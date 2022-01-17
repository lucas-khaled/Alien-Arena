using System;
using UnityEngine;
using AlienArena.Inventory;
using AlienArena.Itens;

namespace AlienArena.Player
{
    public class Player : MonoBehaviour
    {
        public int Coins { get; private set; }
        public float MaxLife { get; private set; }
        public float MaxEnergy { get; private set; }
        public float Velocity { get; private set; }
        
        private float _energy;
        private float _life;

        public Action<float, string> onChangeAtrribute;

        private void Start()
        {
            Inventory.Inventory.instance.onEquip += ChangeEquipStats;
            AddLife(100);
        }

        public void UseEnergy(float usage)
        {
            _energy -= usage;
            onChangeAtrribute?.Invoke(_energy,"Energy");
        }

        public void Damage(float damage)
        {
            _life = Mathf.Clamp(_life - damage, 0, MaxLife);
            onChangeAtrribute?.Invoke(_life, "Life");
        }

        public void AddCoins(int coins)
        {
            Coins += coins;
            onChangeAtrribute?.Invoke(Coins, "Coins");
        }

        public void AddLife(float life)
        {
            MaxLife += life;
            Damage(-life);
            onChangeAtrribute?.Invoke(MaxLife, "MaxLife");
        }

        public void AddVelocity(float velocity)
        {
            Velocity += velocity;
            onChangeAtrribute?.Invoke(MaxLife, "Velocity");
        }

        public void AddMaxEnergy(float energy)
        {
            MaxEnergy += energy;
            UseEnergy(-energy);
            onChangeAtrribute?.Invoke(MaxLife, "MaxEnergy");
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