﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    private Player _player;
    //animator handle
    private Animator _animator;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _explosion_Sfx;


    private void Start()
    {
        //animator getcomponent
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator is NULL!");
        }
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL!");
        }
        _audio = GetComponent<AudioSource>();
        if(_audio == null)
        {
            Debug.LogError("The Enemy audio source is NULL");
        }
    }
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.4f)
        {
            transform.position = new Vector3(Random.Range(-10, 10), 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player.Damage();
            _speed = 1.75f;            
            _animator.SetTrigger("OnEnemyDeath");
            _audio.clip = _explosion_Sfx;
            _audio.Play();
            Destroy(this.gameObject, 2.3f);

        }
        else if (other.tag == "Laser")
        {
            int randomScore = Random.Range(5, 11);
            _player.AddScore(randomScore);
            //trigger explosion anim
            _speed = 1.75f;
            _animator.SetTrigger("OnEnemyDeath");
            _audio.clip = _explosion_Sfx;
            _audio.Play();
            Destroy(this.gameObject, 2.3f);
            Destroy(other.gameObject);
        }
    }
}
    
