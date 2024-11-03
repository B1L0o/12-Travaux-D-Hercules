using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPF : MonoBehaviour
{
    public GameObject _player;
    public bool isRightOriented;
    public float bulletSpeed;
    public int Damage;
    public int _width;
    public int _height;
    public int _groundy;
    private EnemyState _enemyState;
    [SerializeField]
    private bool _DebugMode;
    [SerializeField]
    private GameObject explosionEffect;
    // Start is called before the first frame update
    
    void Start()
    {
        //_player = GameObject.FindWithTag("Player");
        //_enemyState =  new FollowPlayer(_width,_height,bulletSpeed,_DebugMode,_player,gameObject,transform,_groundy,isRightOriented);
        //_enemyState.InitialiseState();
        
    }

    public void Init()
    {
        if (isRightOriented)
        {
            _enemyState =  new FollowPlayer(_width,_height,bulletSpeed,_DebugMode,_player,gameObject,transform,_groundy,isRightOriented);

        }
        else
        {
            _enemyState =  new FollowPlayer(_width,_height,bulletSpeed,_DebugMode,_player,gameObject,_player.transform,_groundy,isRightOriented);
        }
        //_enemyState =  new FollowPlayer(_width,_height,bulletSpeed,_DebugMode,_player,gameObject,transform,_groundy,isRightOriented);
        _enemyState.InitialiseState();
    }

    

    private void Update()
    {
        _enemyState.ApplyState();
        //GetComponent<SpriteRenderer>().flipX = !(GetComponent<SpriteRenderer>().flipX);
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Tilemap"))
        {
            if (col.CompareTag("Player"))
            {
                StartCoroutine(_player.GetComponent<Player>().TakeDMG(Damage));
            }
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
        
    }
}