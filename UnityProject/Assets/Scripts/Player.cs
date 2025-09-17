using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    private Vector3 position;

    public GameObject projectile;
    public float fireRate = 0.1f;
    private float fireTime;

    public float health = 100.0f;

    public GameObject deathEffect;

    private string[] missileKeys = {"q", "e", "r", "t", "g", "f", "v", "c", "x", "z"};

    public GameObject missile;
    public float missilefireRate = 2.0f;
    private float missilefireTime;
    


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        Movement();
        Boundary();

        transform.position = position;

        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time > fireTime)
        {
            GameObject Bullet = Instantiate(projectile, transform.position, transform.rotation);
            Bullet.tag = "Player";
            fireTime = Time.time + fireRate;
        }
        foreach (string key in missileKeys)
        {
            if (Input.GetKey(key) && Time.time > missilefireTime)
            {
                Instantiate(missile, transform.position, transform.rotation);
                GameManager.instance.locatingEnemy = key;
                missilefireTime = Time.time + missilefireRate;
            }
        }
    }

    //Player input controls
    private void Movement()
    {
        //Right movement
        if (Input.GetKey("d"))
        {
            position.x += moveSpeed * Time.deltaTime;
        }

        //Left movement
        if (Input.GetKey("a"))
        {
            position.x -= moveSpeed * Time.deltaTime;
        }

        //Up movement
        if (Input.GetKey("w"))
        {
            position.z += moveSpeed * Time.deltaTime;
        }

        //Down movement
        if (Input.GetKey("s"))
        {
            position.z -= moveSpeed * Time.deltaTime;
        }
    }

    private void Boundary()
    {
        //X Boundary Checks
        if (position.x > GameManager.instance.xBoundary)
        {
            position.x = GameManager.instance.xBoundary;
        }
        else if (position.x < -GameManager.instance.xBoundary)
        {
            position.x = -GameManager.instance.xBoundary;
        }

        //Y Boundary Checks
        if (position.z > GameManager.instance.zBoundary)
        {
            position.z = GameManager.instance.zBoundary;
        }
        else if (position.z < -GameManager.instance.zBoundary)
        {
            position.z = -GameManager.instance.zBoundary;
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            GameManager.instance.alive = false;
            GameManager.instance.sceneRestartDelay = Time.time + 2.0f;
        }
    }
}
