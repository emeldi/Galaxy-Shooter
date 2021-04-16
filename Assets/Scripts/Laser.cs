﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8;
    [SerializeField]
    private bool _isEnemy;

    void Update()
    {
        if (_isEnemy == false)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            if (transform.position.y > 8)
            {
                if (transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    Destroy(this.gameObject);
                }

            }
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if (transform.position.y < -5.5f)
            {

                Destroy(this.gameObject);


            }
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemy == true)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

        }
    }
}
