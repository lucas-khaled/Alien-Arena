using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;

        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject storePanel;
        [SerializeField] private GameObject interactionPanel;
        
        public Action<Store.Store> onInventoryOpen; 
        public Action<Store.Store, Player.Player> onStoreOpen;
        
        private Player.Player _playerRef;

        public void SetInteractionActive(bool active)
        {
            interactionPanel.SetActive(active);
        }
        
        public void OpenInventory(Store.Store store = null)
        {
            onInventoryOpen?.Invoke(store);
            storePanel.SetActive(false);
            inventoryPanel.SetActive(true);

            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);
        }

        public void OpenStore(Store.Store store)
        {
            onStoreOpen?.Invoke(store, _playerRef);
            storePanel.SetActive(true);
            inventoryPanel.SetActive(false);
            
            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);
        }

        public void QuitPause()
        {
            storePanel.SetActive(false);
            inventoryPanel.SetActive(false);
            
            Time.timeScale = 1;
            pauseMenuPanel.SetActive(false);
        }
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            _playerRef = FindObjectOfType<Player.Player>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OpenInventory();
            }
        }
    }
}
