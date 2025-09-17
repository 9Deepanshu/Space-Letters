using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissile : MonoBehaviour {

    public float speed = 100.0f;
    public float rotSpeed = 2.0f;

    private float lifeTime = 8.0f;

    public GameObject[] enemies;
    private GameObject target;
    private GameObject closestEnemy;


    public GameObject explosionEffect;

    public float damage = 100.0f;

    // Use this for initialization
    void Start() 
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag(GameManager.instance.locatingEnemy);

        if(!target)
        {
            target = FindClosestEnemyUnit();
        }
        
        Movement();
    }

    //Algorithm controlling the detection of closest enemy target using enemy list
    //Return the closest enemy in enemyList
    public GameObject FindClosestEnemyUnit() 
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        return closestEnemy;
    }


    private void Movement() 
    {   
        transform.position += transform.forward * speed * Time.deltaTime;

        if (target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }


    void OnTriggerEnter(Collider other) 
    {
        if (other.transform.tag == GameManager.instance.locatingEnemy)
        {
            other.GetComponent<Enemy2>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
