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

        public Action<Store> onStoreOpen;
        
        private Player.Player _playerRef;
        
        private void Start()
        {
            _playerRef = FindObjectOfType<Player.Player>();
        }

        private void Update()
        {
            CheckInteraction();   
        }

        private void CheckInteraction()
        {
            float distance = Vector2.Distance(transform.position, _playerRef.transform.position);
            if (distance < interactionRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    onStoreOpen?.Invoke(store);
                }
            }
        }
    }
}
