using UnityEngine;

namespace AlienArena.Arena
{
    [CreateAssetMenu(fileName = "Arena Settings", menuName = "Alien Arena/Arena/Arena Settings", order = 0)]
    public class ArenaSettings : ScriptableObject
    {
        public Challenge[] challenges;
        public bool isDone { get; set; }
        public Challenge actualChallenge { get; private set; }

        public void SetActualChallenge(int index)
        {
            if(index >= challenges.Length) return;

            actualChallenge = challenges[index];
        }
    }
}