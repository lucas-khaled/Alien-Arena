using System;
using System.Collections;
using System.Collections.Generic;
using AlienArena.Controllers;
using AlienArena.Player;
using AlienArena.UI;
using UnityEngine;

namespace AlienArena.Store
{
    public class Seller : MonoBehaviour, IInteractable
    {
        [SerializeField] private Store store;
        
        public void Interact()
        {
            GamePauseUIController.instance.OpenStore(store);
        }
    }
}
