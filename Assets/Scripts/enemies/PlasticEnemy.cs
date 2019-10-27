using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticEnemy : Enemy
{
    [SerializeField] private float fireRate;
    [SerializeField] private float lastTimeShot;
    public GameObject bullet;

    void Start()
    {
        lastTimeShot = Time.time;
        StartUp();
    }

    void Update()
    {
        if (lastTimeShot + fireRate > Time.time) return;

        GameObject go = Instantiate(bullet, transform);
        go.transform.position = transform.position;
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        go.GetComponent<PlasticBullet>().direction = direction;
        lastTimeShot = Time.time;
    }
}