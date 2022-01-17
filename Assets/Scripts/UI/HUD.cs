using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.UI
{
    public class HUD : MonoBehaviour
    {
        public static HUD instance;
        
        [SerializeField] private Image lifeBar;
        [SerializeField] private Image energyBar;
        [SerializeField] private GameObject interactionPanel;

        private Player.Player player;
        
        public void SetInteractionActive(bool active)
        {
            interactionPanel.SetActive(active);
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }

            instance = this;
        }

        private void Start()
        {
            player = GamePauseUIController.instance.PlayerRef;
            player.onChangeAtrribute += OnPlayerAttributeChange;
        }

        private void OnPlayerAttributeChange(float value, string name)
        {
            switch (name)
            {
                case "Life":
                    lifeBar.fillAmount = value / player.PlayerStats.maxLife;
                    break;
                case "Energy":
                    energyBar.fillAmount = value / player.PlayerStats.maxEnergy;
                    break;
            }
        }
    }
}
