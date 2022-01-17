using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlienArena.Arena;
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
        [SerializeField] private GameObject arenaUIPanel;
        

        public Action<Store.Store> onInventoryOpen; 
        public Action<Store.Store, Player.Player> onStoreOpen;
        public Action<ArenaSettings> onArenaOpened;
        
        public Player.Player PlayerRef { get; private set; }

        private Store.Store _openedStore;

        
        public void OpenInventory(Store.Store store = null)
        {
            onInventoryOpen?.Invoke(store);

            List<string> panels = new List<string>();
            panels.Add("Inventory");
            
            if(store != null)
                panels.Add("Switch");
            
            OpenPanels(panels.ToArray());

            Time.timeScale = 0;
        }

        public void OpenStore(Store.Store store)
        {
            onStoreOpen?.Invoke(store, PlayerRef);
            OpenPanels("Store", "Switch");
            
            Time.timeScale = 0;
            _openedStore = store;
        }

        public void OpenArena(ArenaSettings settings)
        {
            onArenaOpened?.Invoke(settings);
            OpenPanels("Arena");

            Time.timeScale = 0;
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

        private void OpenPanels(params string[] panelNames)
        {
            storePanel.SetActive(panelNames.Contains("Store"));
            inventoryPanel.SetActive(panelNames.Contains("Inventory"));
            arenaUIPanel.SetActive(panelNames.Contains("Arena"));
            switchToggleGroup.gameObject.SetActive(panelNames.Contains("Switch"));
            
            pauseMenuPanel.SetActive(true);
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
            
            coinsText.SetText("Coins: "+PlayerRef.PlayerStats.coins);
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
