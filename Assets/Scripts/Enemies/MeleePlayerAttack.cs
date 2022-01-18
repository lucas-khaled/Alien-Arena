using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena.Enemies
{
    public class MeleePlayerAttack : MonoBehaviour
    {
        [SerializeField] private float rangeAttack = 2;
        [SerializeField] private float attackRate = 3;
        [SerializeField] private float damage = 10;

        private Player.Player _player;
        private Animator _animator;
        private float attackTime;
        private void Start()
        {
            _player = Player.Player.instance;
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            attackTime += Time.deltaTime;
            CheckAttack();
        }

        private void CheckAttack()
        {
            float distance = Vector3.Distance(transform.position, _player.transform.position);
            if(distance < rangeAttack && attackTime >= attackRate)
                Attack();
        }

        private void Attack()
        {
            _player.Damage(damage);
            attackTime = 0;
            
            if(_animator != null)
                _animator.SetTrigger("Attack");
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, rangeAttack);
        }
    }
}
