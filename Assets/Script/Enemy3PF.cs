using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3PF : Enemy
{
    [SerializeField]
    private int _width;
    [SerializeField]
    private int _height;
    [SerializeField]
    private int _groundy;
    private bool canShoot;
    [SerializeField]
    private Transform firePos;
    [SerializeField]
    private GameObject bullet;
    private Animator _animator;
    [SerializeField]
    private float range;

    protected void Update()
    {
        if (canShoot&&Vector2.Distance(_player.transform.position,transform.position)<range&&Mathf.Abs(_player.transform.position.y-transform.position.y)<1 && IsLookingAtSomething(_player.transform))
        {
           
            _animator.Play("ennemi3_shootPF", -1, 0f);
            canShoot = false;
        }
    }

    protected void ShootPF()
    {
        GameObject fireBall =  Instantiate(bullet,firePos.position,transform.rotation);
        FireballPF fireballObject = fireBall.GetComponent<FireballPF>();
        fireballObject.Damage = Damage;
        fireballObject._player = _player.gameObject;
        fireballObject._groundy = _groundy;
        fireballObject._height = _height;
        fireballObject._width = _width;
        fireballObject.bulletSpeed = _speed;
        fireballObject.isRightOriented = isRightOriented;
        fireballObject.Init();
        fireBall.SetActive(true);

    }
    
    protected void Start()
    {
        canShoot = true;
        _animator = GetComponent<Animator>();
    }
    
}