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
        private float attackTime;
        private void Start()
        {
            _player = FindObjectOfType<Player.Player>();
        }

        private void Update()
        {
            attackTime += Time.deltaTime;
            CheckAttack();
        }

        private void CheckAttack()
        {
            float distance = Vector3.Distance(_player.transform.position, _player.transform.position);
            if(distance < rangeAttack && attackTime >= attackRate)
                Attack();
        }

        private void Attack()
        {
            _player.Damage(damage);
            attackTime = 0;
        }
    }
}
