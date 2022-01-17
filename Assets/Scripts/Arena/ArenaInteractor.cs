using AlienArena.Interaction;
using AlienArena.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlienArena.Arena
{
    public class ArenaInteractor : MonoBehaviour, IInteractable
    {
        [SerializeField] private ArenaSettings settings;
        
        public void Interact()
        {
            GamePauseUIController.instance.OpenArena(settings);
        }
    }
}