using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena
{
    public class GamePauseUIController : MonoBehaviour
    {
        public static GamePauseUIController instance;

        [Header("UI's")]
        [SerializeField] private ToggleGroup switchToggleGroup;
        [SerializeField] private TMP_Text coinsText;
        
        [Header("GameObjects")]
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject storePanel;
        [SerializeField] private GameObject interactionPanel;

        public Action<Store.Store> onInventoryOpen; 
        public Action<Store.Store, Player.Player> onStoreOpen;
        
        public Player.Player PlayerRef { get; private set; }

        private Store.Store _openedStore;

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
            onStoreOpen?.Invoke(store, PlayerRef);
            storePanel.SetActive(true);
            inventoryPanel.SetActive(false);
            switchToggleGroup.gameObject.SetActive(true);
            
            Time.timeScale = 0;
            pauseMenuPanel.SetActive(true);

            _openedStore = store;
        }

        public void QuitPause()
        {
            storePanel.SetActive(false);
            inventoryPanel.SetActive(false);
            switchToggleGroup.gameObject.SetActive(false);
            
            Time.timeScale = 1;
            pauseMenuPanel.SetActive(false);

            _openedStore = null;
        }

        public void ToggleSwitched(bool active)
        {
            if(!active) return;
            
            if (switchToggleGroup.GetFirstActiveToggle().name.Contains("Store"))
                OpenStore(_openedStore);
            else
                OpenInventory(_openedStore);
        }
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            PlayerRef = FindObjectOfType<Player.Player>();
        }

        private void Start()
        {
            PlayerRef.onChangeAtrribute += PlayerAtributteChange;
            
            coinsText.SetText("Coins: "+PlayerRef.Coins);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OpenInventory();
            }
        }

        private void PlayerAtributteChange(float value, string name)
        {
            if (name == "Coins")
                coinsText.SetText("Coins: "+value);
        }
    }
}
