using System.Linq;
using AlienArena.Enemies;
using UnityEngine;

namespace AlienArena.Arena
{
    [CreateAssetMenu(fileName = "Challenge", menuName = "Alien Arena/Arena/Challenge")]
    public class Challenge : ScriptableObject
    {
        public ArenaEnemyData[] enemies;
        public float minInstanceTime = 1;
        public float maxInstanceTime = 3;
        public int coinsReward = 100;
        public string challengeName;

        public bool completed;

        public int GetEnemiesTotalCount()
        {
            return enemies.Sum(enemyData => enemyData.quantity);
        }
        
    }

    [System.Serializable]
    public struct ArenaEnemyData
    {
        public Enemy enemy;
        [Min(1)] public int quantity;
    }
}