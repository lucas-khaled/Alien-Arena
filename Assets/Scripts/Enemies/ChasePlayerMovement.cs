using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ChasePlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 3;
        [SerializeField] private float stopRange = 3;
        
        private Player.Player _player;
        private Rigidbody2D _rigidbody;

        private Vector2 _lookDirection;
        
        public void Start()
        {
            _player = Player.Player.instance;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _lookDirection = _player.transform.position - transform.position;
            Rotate();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float distance = Vector2.Distance(transform.position, _player.transform.position);
            
            if(distance < stopRange) return;

            
            Vector2 newPos = (Vector2)(_rigidbody.position + _lookDirection.normalized * (speed * Time.deltaTime));
            _rigidbody.MovePosition(newPos);
            
            
            float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = angle;
        }

        private void Rotate()
        {
            float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = angle;
        }
    }
}
