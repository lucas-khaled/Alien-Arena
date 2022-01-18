using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlienArena
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [SerializeField] private float lerp = 0.5f;
        private Player.Player _player;

        private void Start()
        {
            _player = Player.Player.instance;
        }

        private void Update()
        {
            transform.position = Vector2.Lerp(transform.position, _player.transform.position, lerp * Time.deltaTime);
        }
        
        
    }
}
