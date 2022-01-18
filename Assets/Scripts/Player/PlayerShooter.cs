using System;
using AlienArena.Inventory;
using AlienArena.Itens;
using UnityEngine;

namespace AlienArena.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private Transform fireTransform;

        private Equipper _equipper;
        private float _fireTime = 0;

        private void Awake()
        {
            _equipper = FindObjectOfType<Equipper>();
        }

        private void Update()
        {
            _fireTime += Time.deltaTime;
            if (Time.timeScale == 1 && Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Weapon weapon = _equipper.GetSlotByType(typeof(Weapon)).data.item as Weapon;
            if(weapon == null) return;
            
            if(_fireTime<weapon.fireRate) return;

            if (weapon.bullets <= 1)
            {
                InstantiateBullet(weapon, fireTransform.position);
                return;
            }

            float bulletRange = 0.5f;
            Vector2 leftRangePoint = fireTransform.position - transform.right * bulletRange;
            Vector2 rightRangePoint = fireTransform.position + transform.right * bulletRange;
            Vector2 rangeVector = rightRangePoint - leftRangePoint; 
            
            for (int i = 0; i < weapon.bullets; i++)
            {
                Vector2 position = leftRangePoint + rangeVector * i / (weapon.bullets - 1);
                InstantiateBullet(weapon,position);
            }

            _fireTime = 0;

        }

        private void InstantiateBullet(Weapon weapon, Vector3 position)
        {
            Projectile projectile = Instantiate(weapon.projectile, position, fireTransform.rotation);
            projectile.damage = weapon.damage;
        }
    }
}