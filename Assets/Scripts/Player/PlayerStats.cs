using UnityEngine;

namespace AlienArena.Player
{
    [CreateAssetMenu(fileName = "Player Stats", menuName = "Alien Arena/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public int coins = 0;
        [SerializeField] private float baseMaxLife = 100;
        [SerializeField] private float baseMaxEnergy = 300;
        [SerializeField] private float baseVelocity = 3;
        
        public float energy;
        public float life;

        public float AddedMaxLife { get; set; }
        public float AddedMaxEnergy { get; set; }
        public float AddedVelocity { get; set; }

        public float MaxLife => baseMaxLife + AddedMaxLife;
        public float MaxEnergy => baseMaxEnergy + AddedMaxEnergy;
        public float Velocity => baseVelocity + AddedVelocity;

        public float BaseMaxEnergy => baseMaxEnergy;
        public float BaseMaxLife => baseMaxLife;
    }
}