using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3.0f;

    public float moveSpeed = 350.0f;

    public float damage = 100.0f;

    public float health = 100.0f;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += Time.deltaTime * moveSpeed * transform.forward;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy" && transform.tag == "Player")
        {
            other.GetComponent<Enemy>().takeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (other.transform.tag == "Player" && transform.tag == "Enemy")
        {
            other.GetComponent<Player>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }
}
