using AlienArena.UI;
using UnityEngine;

namespace AlienArena.Interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private LayerMask mask;
        [SerializeField] private KeyCode key;
        [SerializeField] private float range;

        private IInteractable _interactable;
        
        private void Start()
        {
            InvokeRepeating("CheckingInteraction", 0, 0.1f);
        }

        private void Update()
        {
            if (_interactable != null && Input.GetKeyDown(key))
            {
                _interactable.Interact();
                HUD.instance.SetInteractionActive(false);
            }
        }

        private void CheckingInteraction()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.up, Mathf.Infinity, mask);

            _interactable = null;
            float distance = float.MaxValue;
            foreach (var hit in hits)
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                if(interactable == null) continue;

                if(Vector2.Distance(hit.transform.position, transform.position) > distance) continue;
                
                _interactable = interactable;
            }

            HUD.instance.SetInteractionActive(_interactable != null);
        }
    }
}
