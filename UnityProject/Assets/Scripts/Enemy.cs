using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 15.0f;
    public float health = 100.0f;

    public float damage = 25.0f;
    private float damageRate = 0.2f;
    private float damageTime;

    public GameObject projectile;
    public float fireRate = 0.1f;
    private float fireTime;

    public  GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    //Enemy Movement - beeline for player
    private void Movement()
    {
        if (GameManager.instance.player)
        {
            transform.LookAt(GameManager.instance.player.transform.position);
        }
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.instance.Score += 1;
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && Time.time > damageTime)
        {
            other.transform.GetComponent<Player>().takeDamage(damage);
            damageTime = Time.time + damageRate;
        }
    }

      private void Shoot()
    {
        if (Time.time > fireTime)
        {   
            GameObject Bullet = Instantiate(projectile, transform.position, transform.rotation);
            Bullet.tag = "Enemy";
            fireTime = Time.time + fireRate;
        }
    }
}
