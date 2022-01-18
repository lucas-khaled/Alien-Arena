using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena.Arena
{
    [RequireComponent(typeof(Collider2D))]
    public class EnergyRecharger : MonoBehaviour
    {
        [SerializeField] private int rechargeAmount = 100; 
        private void Start()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<Player.Player>().UseEnergy(-rechargeAmount);
                Destroy(gameObject);
            }
        }
    }
}
