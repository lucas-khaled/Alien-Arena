using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArena.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Image lifeBar;
        [SerializeField] private Image energyBar;

        private Player.Player player;
        
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
                    lifeBar.fillAmount = value / player.MaxLife;
                    break;
                case "Energy":
                    energyBar.fillAmount = value / player.MaxEnergy;
                    break;
            }
        }
    }
}
