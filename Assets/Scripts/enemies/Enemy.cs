using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int damage;
    protected int health;

    protected Transform target;

    protected void StartUp()
    {
        target = GameObject.Find("Player").transform;
        health = 1;
    }

}