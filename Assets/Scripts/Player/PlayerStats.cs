using UnityEngine;

namespace AlienArena.Player
{
    [CreateAssetMenu(fileName = "Player Stats", menuName = "Alien Arena/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        public int coins = 0;
        public float maxLife = 100;
        public float maxEnergy = 300;
        public float velocity = 3;
        
        public float energy;
        public float life;
    }
}