using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float maxLife = 100;

        public Action<Enemy> onEnemyDeath;
        
        private float _life;

        public void Damage(float amount)
        {
            _life -= amount;
            
            if(_life <= 0) 
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
            onEnemyDeath?.Invoke(this);
        }
        
        protected void Start()
        {
            _life = maxLife;
        }
    }
}
