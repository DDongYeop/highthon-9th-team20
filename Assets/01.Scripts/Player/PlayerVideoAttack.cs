using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVideoAttack : MonoBehaviour
{
    private PlayerAttack _player;

    private void Awake()
    {
        _player = transform.root.GetComponent<PlayerAttack>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<EnemyMono>(out EnemyMono enemy))
        {
            //enemy.CurrentHP -= _player.dam
        }
    }
}
