using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        
        [SerializeField] private TMP_Text lifeText;
        [SerializeField] private TMP_Text energyText;

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
            player = Player.Player.instance;
            player.onChangeAtrribute += OnPlayerAttributeChange;
        }

        private void OnPlayerAttributeChange(float value, string name)
        {
            switch (name)
            {
                case "Life":
                    lifeBar.fillAmount = value / player.PlayerStats.MaxLife;
                    lifeText.SetText(value + " / "+player.PlayerStats.MaxLife);
                    break;
                case "Energy":
                    energyBar.fillAmount = value / player.PlayerStats.MaxEnergy;
                    energyText.SetText(value + " / "+player.PlayerStats.MaxEnergy);
                    break;
            }
        }
    }
}
