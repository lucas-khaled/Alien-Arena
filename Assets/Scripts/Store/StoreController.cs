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
        

        private void Start()
        {
            InvokeRepeating("CheckInteraction", 0, 0.2f);   
        }

        private void CheckInteraction()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, interactionRange, Vector3.back, out hit, LayerMask.GetMask("Player")));
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UIController.instance.OpenStore(store);
                }
            }
        }
    }
}
