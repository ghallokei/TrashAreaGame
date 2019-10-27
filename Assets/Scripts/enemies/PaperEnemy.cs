using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaperEnemy : Enemy
{
    [SerializeField] public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartUp();
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("player")) return;

        GameObject player = GameObject.FindWithTag("player");
        player.GetComponent<Player>().TakeDamage(10);
    }
}