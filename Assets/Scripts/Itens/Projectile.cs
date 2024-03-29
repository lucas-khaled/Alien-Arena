using System;
using System.Collections;
using System.Collections.Generic;
using AlienArena.Enemies;
using UnityEngine;

namespace AlienArena.Itens
{
    [RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject effectOnHit;
        [SerializeField] private float speed = 20f;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        
        public float damage { get; set; }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        private void Start()
        {
            Destroy(gameObject,5);
        }

        private void FixedUpdate()
        {
            Vector2 bulletDirection = transform.up * (speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_rigidbody.position + bulletDirection);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(effectOnHit != null)
                Instantiate(effectOnHit, transform.position, transform.rotation);

            if (other.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.Damage(damage);
            }
            
            Destroy(gameObject);
        }
    }
}
