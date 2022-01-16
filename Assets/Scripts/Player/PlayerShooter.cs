using System;
using AlienArena.Inventory;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform fireTransform;
        [SerializeField] private float fireRate = 0.2f;

        private Equipper _equipper;
        private float _fireTime = 0;

        private void Awake()
        {
            _equipper = FindObjectOfType<Equipper>();
        }

        private void Update()
        {
            _fireTime += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if(_fireTime<fireRate) return;

            Weapon weapon = _equipper.GetSlotByType(typeof(Weapon)).item as Weapon;

            for (int i = 0; i < weapon.projectileQuantity; i++)
            {
                Projectile projectile = Instantiate(weapon.projectile, fireTransform.position, fireTransform.rotation);
                projectile.damage = weapon.damage;
            }
            
        }
    }
}