using AlienArena.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlienArena.Arena
{
    public class ArenaInteractor : MonoBehaviour, IInteractable
    {
        [SerializeField] private ArenaSettings settings;
        
        public void Interact()
        {
            StartArenaChallenge(0);
        }

        public void StartArenaChallenge(int index)
        {   
            settings.SetActualChallenge(index);
            SceneManager.LoadScene("Arena");
        }
    }
}