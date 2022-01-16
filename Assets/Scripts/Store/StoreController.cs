using System;
using System.Collections;
using System.Collections.Generic;
using AlienArena.Player;
using UnityEngine;

namespace AlienArena.Store
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private float interactionRange = 1f;
        [SerializeField] private Store store;

        private Player.Player _player;
        private bool _inRange = false;

        private void Start()
        {
            InvokeRepeating("CheckInteraction", 0, 0.2f);
            _player = FindObjectOfType<Player.Player>();
        }

        private void CheckInteraction()
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < interactionRange)
            {
                UIController.instance.SetInteractionActive(true);
                _inRange = true;
            }
            else
            {
                UIController.instance.SetInteractionActive(false);
                _inRange = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && _inRange)
                UIController.instance.OpenStore(store);
        }
    }
}
