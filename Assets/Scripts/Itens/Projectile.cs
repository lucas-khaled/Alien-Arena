using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena.Itens
{
    [RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private GameObject effectOnHit;
        [SerializeField] private float speed = 20f;

        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider.isTrigger = true;
        }

        private void Start()
        {
            Destroy(gameObject,20);
        }

        private void FixedUpdate()
        {
            Vector2 bulletDirection = transform.up * (speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_rigidbody.position + bulletDirection);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Instantiate(effectOnHit, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
